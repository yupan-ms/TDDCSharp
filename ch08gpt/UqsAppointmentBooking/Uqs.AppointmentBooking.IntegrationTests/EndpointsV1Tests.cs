using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Uqs.AppointmentBooking.Api;

namespace Uqs.AppointmentBooking.IntegrationTests;

[Collection("Sequential")]
public class EndpointsV1Tests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    protected readonly HttpClient _httpClient;

    public EndpointsV1Tests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    private async Task Swagger_Document_Exposed()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/swagger/v1/swagger.json");

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
