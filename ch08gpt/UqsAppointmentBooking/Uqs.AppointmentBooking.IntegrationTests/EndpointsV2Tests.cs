using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Gherkin.Quick;

namespace Uqs.AppointmentBooking.IntegrationTests;

[Collection("Sequential")]
public class EndpointsV2Tests :  IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    protected readonly HttpClient _httpClient;

    public EndpointsV2Tests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }
    
    [Fact]
    private async Task Swagger_Document_Exposed()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/swagger/v2/swagger.json");

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [FeatureFile("./Gherkin/story_1_services_selection.feature")]
    public sealed class Customer_Selects_A_Service_For_Booking : Feature
    {
        private HttpClient _client;
        public Customer_Selects_A_Service_For_Booking(EndpointsV2Tests parent) 
        { 
            this._client = parent._httpClient;
        }

        [Given(@"the customer is on the services selection page")]
        public void Given_the_customer_is_on_the_services_selection_page()
        {
            
        }
    
        [When(@"the customer views the list of available services")]
        public void The_customer_views_the_list_of_available_services()
        {           

        }

        [Then(@"the customer should see the cost of each service")]
        public void The_customer_should_see_the_cost_of_each_service()
        {

        }

        [And(@"the customer should be able to select a service")]
        public void The_customer_should_be_able_to_select_a_service()
        {

        }

        [When(@"the customer selects a service")]
        public void The_customer_selects_a_service()
        {

        }

        [Then(@"the customer should be transferred to the booking page")]
        public void The_customer_should_be_transferred_to_the_booking_page()
        {

        }
    }
}