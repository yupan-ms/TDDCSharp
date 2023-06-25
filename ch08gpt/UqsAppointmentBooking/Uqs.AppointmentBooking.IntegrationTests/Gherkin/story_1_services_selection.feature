# gpt-4

Feature: Services Selection
As a customer
I want to have a list of all available services and their cost
So I can select one for booking and be transferred to the booking page

Background:
Given the customer is on the services page

@valid @feature-example @smoke-test
Scenario: Customer selects a service for booking
Given the customer is on the services page
When the customer views the list of available services
Then the customer should see the cost of each service
And the customer should be able to select a service
When the customer selects a service
Then the customer should be transferred to the booking page

@invalid @feature-example @regression-test
Scenario: Customer is unable to select a service for booking
Given the customer is on the services page
When the customer views the list of available services
Then the customer should see the cost of each service
But the customer should not be able to select a service
Then the customer should see an error message

# The feature file can be named as "services_selection.feature". The feature describes the ability of the customer to view and select a service from the list of available services and their cost. The background sets the context that the customer is on the services page. The first scenario covers the positive scenario where the customer is able to select a service and is transferred to the booking page. The second scenario covers the negative scenario where the customer is unable to select a service and sees an error message. The scenarios are tagged appropriately for easy filtering and execution.