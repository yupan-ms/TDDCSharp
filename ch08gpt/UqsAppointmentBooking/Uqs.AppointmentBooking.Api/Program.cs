using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Uqs.AppointmentBooking.Domain;
using Uqs.AppointmentBooking.Domain.Services;
using Uqs.AppointmentBooking.Infrastructure.Repositories;
using Uqs.AppointmentBooking.Api.EndpointsV1;
using Uqs.AppointmentBooking.Api.EndpointsV2;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
    });
    // Use method name as operationId
    options.CustomOperationIds(apiDesc =>
    {
        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new OpenApiInfo()
    {
        Version = "v2",
    });

    //options.OperationFilter<SwaggerOperationFilter>();
    options.DocumentFilter<SwaggerDocumentFilter>();
    options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    options.UseSqlite(String.Format("Data Source={0}", Path.Join(path, "AppointmentBooking.db")));
});

// Add services to the container.
builder.Services.AddScoped<INowService, NowService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<ISlotsService, SlotsService>();

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(nameof(ApplicationSettings)));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
db?.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        options.RoutePrefix = string.Empty;
    });
}

app.MapGroup("/appointmentbooking/v1")
.WithGroupName("v1")
.MapApiV1()
.WithTags("Endpoints");

app.MapGroup("/appointmentbooking/v2")
.WithGroupName("v2")
.MapApiV2()
.WithTags("Endpoints");

app.Run();

public class SwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // var apiVersion = context.ApiDescription.GroupName;
    }
}

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        document.Info.Title = "Uqs Appointment Booking Api";
        document.Info.Description = "A booking solution that aims to do the following: \n" + 
        " • Market the available hairdressing services. \n" + 
        " • Allow a customer to book an appointment with a specific or a random barber. \n" +
        " • Give barbers a rest between appointments, usually 5 minutes. \n" +
        " • Barbers have various shifts in the shop and they are off work on different days, so the solution should take care of picking free slots based on the availability of barbers. \n" +
        " • Time saving by not having to arrange appointments on the phone or in person. \n";
        document.Info.Contact = new OpenApiContact()
        {
            Name = "Author",
            Url = new Uri("https://bing.com")
        };
    }
}

public partial class Program { }