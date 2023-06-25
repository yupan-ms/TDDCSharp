using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Uqs.AppointmentBooking.Contract;
using Uqs.AppointmentBooking.Domain.Services;
using Uqs.AppointmentBooking.Infrastructure.Repositories;

namespace Uqs.AppointmentBooking.Api.EndpointsV1;

public static class EndpointsV1
{
    public static RouteGroupBuilder MapApiV1(this RouteGroupBuilder group)
    {
        group.MapGet("/employees", GetAvailableEmployees)
        .Produces<AvailableEmployees>();

        group.MapGet("/services/{id}", GetService)
        .Produces<Service>();

        group.MapGet("/services", GetAvailableServices)
        .Produces<AvailableServices>();

        group.MapGet("/slots/{serviceId}/{employeeId}", GetAvailableSlots)
        .Produces<AvailableSlots>();

        return group;
    }

    //[HttpGet]
    static async Task<IResult> GetAvailableEmployees(IEmployeesService employeesService)
    {
        throw new NotImplementedException();
    }

    //[HttpGet]
    static async Task<IResult> GetAvailableServices(IServicesService servicesService)
    {
        throw new NotImplementedException();
    }

    //[HttpGet("{serviceId:int}")]
    static async Task<IResult> GetService(int id, IServicesService servicesService)
    {
        throw new NotImplementedException();
    }

    //[HttpGet]
    static async Task<IResult> GetAvailableSlots(int serviceId, int? employeeId, ISlotsService slotsService)
    {
        throw new NotImplementedException();
    }
}