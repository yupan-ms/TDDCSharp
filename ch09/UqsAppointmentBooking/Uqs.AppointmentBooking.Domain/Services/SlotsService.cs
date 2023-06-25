﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Uqs.AppointmentBooking.Domain.Database;
using Uqs.AppointmentBooking.Domain.DomainObjects;
using Uqs.AppointmentBooking.Domain.Report;

namespace Uqs.AppointmentBooking.Domain.Services;

public interface ISlotsService
{
    Task<Slots> GetAvailableSlotsForEmployee(int serviceId, int employeeId);
}

public class SlotsService : ISlotsService
{
    private readonly ApplicationContext _context;
    private readonly ApplicationSettings _settings;
    private readonly DateTime _now;
    private readonly TimeSpan _roundingIntervalSpan;

    public SlotsService(ApplicationContext context, INowService nowService, IOptions<ApplicationSettings> settings)
    {
        _context = context;
        _settings = settings.Value;
        _roundingIntervalSpan = TimeSpan.FromMinutes(_settings.RoundUpInMin);
        _now = RoundUpToNearest(nowService.Now);
    }

    public async Task<Slots> GetAvailableSlotsForEmployee(int serviceId, int employeeId)
    {
        var service = await _context.Services!.SingleOrDefaultAsync(x => x.Id == serviceId);
        if (service is null)
        {
            throw new ArgumentException("Record not found", nameof(serviceId));
        }
        var isEmpFound = await _context.Employees!.AnyAsync(x => x.Id == employeeId);
        if (!isEmpFound)
        {
            throw new ArgumentException("Record not found", nameof(employeeId));
        }
        var appointmentsMaxDay = GetEndOfOpenAppointments();

        List<(DateTime From, DateTime To)> timeIntervals = new();

        var shifts = _context.Shifts!.Where(
            x => x.EmployeeId == employeeId && 
            x.Ending < appointmentsMaxDay &&
            ((x.Starting <= _now && x.Ending > _now) || x.Starting > _now));
        
        foreach(var shift in shifts)
        {
            var potentialAppointmentStart = shift.Starting;
            var potentialAppointmentEnd = 
                potentialAppointmentStart.AddMinutes(service.AppointmentTimeSpanInMin);
            
            for(int increment = 0;potentialAppointmentEnd <= shift.Ending;increment += _settings.RoundUpInMin)
            {
                potentialAppointmentStart = shift.Starting.AddMinutes(increment);
                potentialAppointmentEnd = potentialAppointmentStart.AddMinutes(service.AppointmentTimeSpanInMin);
                if (potentialAppointmentEnd <= shift.Ending)
                {
                    timeIntervals.Add((potentialAppointmentStart, potentialAppointmentEnd));
                }
            }
        }

        var appointments = _context.Appointments!.Where(x => x.EmployeeId == employeeId &&
            x.Ending < appointmentsMaxDay &&
            ((x.Starting <= _now && x.Ending > _now) || x.Starting > _now)).ToArray();

        foreach(var appointment in appointments)
        {
            DateTime appointmentStartWithRest = appointment.Starting.AddMinutes(-_settings.RestInMin);
            DateTime appointmentEndWithRest = appointment.Ending.AddMinutes(_settings.RestInMin);
            timeIntervals.RemoveAll(x =>
                IsPeriodIntersecting(x.From, x.To, appointmentStartWithRest, appointmentEndWithRest));
        }

        var uniqueDays = timeIntervals
            .DistinctBy(x => x.From.Date)
            .Select(x => x.From.Date);

        var daySlotsList = new List<DaySlots>();

        foreach(var day in uniqueDays)
        {
            var startTimes = timeIntervals.Where(x => x.From.Date == day.Date).Select(x => x.From).ToArray();
            var daySlots = new DaySlots(day, startTimes);
            daySlotsList.Add(daySlots);
        }

        var slots = new Slots(daySlotsList.ToArray());

        return slots;
    }

    private DateTime GetEndOfOpenAppointments() => _now.Date.AddDays(
        _settings.OpenAppointmentInDays);

    private DateTime RoundUpToNearest(DateTime dt)
    {
        var ticksInSpan = _roundingIntervalSpan.Ticks;
        return new DateTime((dt.Ticks + ticksInSpan - 1) 
            / ticksInSpan * ticksInSpan, dt.Kind);
    }

    private bool IsPeriodIntersecting(DateTime fromT1, DateTime toT1, DateTime fromT2, DateTime toT2) 
        => fromT1 < toT2 && toT1 > fromT2;
}