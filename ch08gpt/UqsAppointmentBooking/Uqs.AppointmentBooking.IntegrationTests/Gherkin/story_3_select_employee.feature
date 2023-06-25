# gpt-4

Feature: Select Employee
As a customer
I want to select any employee or a specific employee for my appointment
So I can pick my favorite barber if I have one

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario Outline: Customer selects an employee for appointment
When the customer selects <employee> for the appointment
Then the appointment should be booked with <employee>

Examples:
  | employee     |
  | Any employee |
  | John         |
  | Jane         |
@invalid @feature-example @regression-test
Scenario: Customer is unable to select an employee for appointment
When the customer tries to select an employee for the appointment
Then the customer should see an error message

# The feature file can be named as "select_employee.feature". The feature describes the ability of the customer to select an employee for the appointment. The background sets the context that the customer is on the booking page. The first scenario covers the positive scenario where the customer is able to select an employee for the appointment using a scenario outline and examples table to test multiple employees. The second scenario covers the negative scenario where the customer is unable to select an employee and sees an error message. The scenarios are tagged appropriately for easy filtering and execution.

