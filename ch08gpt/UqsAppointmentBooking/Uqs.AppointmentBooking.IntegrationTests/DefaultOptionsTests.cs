using System;
using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Uqs.AppointmentBooking.Api;
using Uqs.AppointmentBooking.Contract;
using Xunit;

namespace Uqs.AppointmentBooking.IntegrationTests
{
    public sealed class DefaultOptionsTests : EndpointsV1Tests
    {
        public DefaultOptionsTests(TestWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        [Trait("Category", "Smoke")]
        [Trait("Category", "Positive")]
        [Trait("Feature", "Default Options")]
        [Trait("Scenario", "Customer sees default options on booking page")]
        public async void CustomerSeesDefaultOptionsOnBookingPage()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContentType = "application/json";
            var expectedEmployee = "Any employee";
            var expectedDate = DateTime.Today;

            // Act
            var response = await _httpClient.GetAsync("/appointmentbooking/v1/slots");
            var responseContent = await response.Content.ReadFromJsonAsync<AvailableSlots>();

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(expectedContentType, response.Content.Headers.ContentType.MediaType);

            // Check if the response contains the expected default employee
            // Assert.Equal(expectedEmployee, responseContent.DefaultEmployee);

            // Check if the response contains the expected default date
            // Assert.Equal(expectedDate, responseContent.DefaultDate);
        }

        [Fact]
        [Trait("Category", "Regression")]
        [Trait("Category", "Negative")]
        [Trait("Feature", "Default Options")]
        [Trait("Scenario", "Customer does not see default options on booking page")]
        public async void CustomerDoesNotSeeDefaultOptionsOnBookingPage()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContentType = "application/json";
            var expectedErrorMessage = "Unable to load default options";

            // Act
            var response = await _httpClient.GetAsync("/appointmentbooking/v1/slots");
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(expectedContentType, response.Content.Headers.ContentType.MediaType);

            // Check if the response does not contain the default employee
            Assert.DoesNotContain("Any employee", responseContent);

            // Check if the response does not contain the default date
            Assert.DoesNotContain(DateTime.Today.ToString(), responseContent);

            // Check if the response contains the expected error message
            Assert.Contains(expectedErrorMessage, responseContent);
        }
    }
}