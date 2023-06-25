using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Uqs.AppointmentBooking.Api;

namespace Uqs.AppointmentBooking.IntegrationTests;

public sealed class ServicesSelectionTests : EndpointsV1Tests
{
    public ServicesSelectionTests(TestWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    [Trait("Category", "Smoke")]
    [Trait("Category", "Positive")]
    [Trait("Feature", "Services Selection")]
    [Trait("Scenario", "Customer selects a service for booking")]
    public async Task CustomerSelectsServiceForBooking()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContentType = "application/json";
        var expectedServiceCost = 50.0;
        var expectedBookingPageUrl = "https://example.com/booking";

        // Act
        var response = await _httpClient.GetAsync("/appointmentbooking/v1/services");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.Equal(expectedContentType, response.Content.Headers.ContentType.MediaType);

        // Check if the response contains the expected service cost
        Assert.Contains($"\"price\":{expectedServiceCost}", responseContent);

        // Check if the response contains the expected booking page URL
        Assert.Contains(expectedBookingPageUrl, responseContent);
    }

    [Fact]
    [Trait("Category", "Regression")]
    [Trait("Category", "Negative")]
    [Trait("Feature", "Services Selection")]
    [Trait("Scenario", "Customer is unable to select a service for booking")]
    public async Task CustomerIsUnableToSelectServiceForBooking()
    {
        // Arrange
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContentType = "application/json";
        var expectedErrorMessage = "Unable to select service for booking";

        // Act
        var response = await _httpClient.GetAsync("/appointmentbooking/v1/services");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.Equal(expectedContentType, response.Content.Headers.ContentType.MediaType);

        // Check if the response does not contain the booking page URL
        Assert.DoesNotContain("booking", responseContent);

        // Check if the response contains the expected error message
        Assert.Contains(expectedErrorMessage, responseContent);
    }
}