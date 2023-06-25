# Copilot
@feature-booking
Feature: Booking an appointment
  As a customer
  I want to book an appointment for a hairdressing service
  So that I can save time and avoid arranging appointments in person or on the phone

  @valid @default-options
  Scenario: Default options selected on booking page
    Given today is "2022-01-01"
    When I go to the booking page
    Then I should see the following default options selected:
      | Employee | Date       |
      | Any      | 2022-01-01 |
    And I should be able to select a service and book an appointment

# gpt-4
Feature: Default Options
As a customer
I want to have a booking page with [Any employee] and today’s date selected by default
So I spend less time clicking and finish booking faster

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer sees default options on booking page
Then the customer should see [Any employee] selected by default
And the customer should see today's date selected by default

@invalid @feature-example @regression-test
Scenario: Customer does not see default options on booking page
Then the customer should not see [Any employee] selected by default
And the customer should not see today's date selected by default
Then the customer should see an error message

# The feature file can be named as "default_options.feature". The feature describes the ability of the customer to see the default options on the booking page. The background sets the context that the customer is on the booking page. The first scenario covers the positive scenario where the customer sees the default options of [Any employee] and today's date selected. The second scenario covers the negative scenario where the customer does not see the default options and sees an error message. The scenarios are tagged appropriately for easy filtering and execution.