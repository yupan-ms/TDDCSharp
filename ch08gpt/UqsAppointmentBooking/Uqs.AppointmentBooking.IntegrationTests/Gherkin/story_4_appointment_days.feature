#gpt-4

Feature: Appointment Days
As a business
We want to present the customer with a 7-day window max, including the current day, to pick an appointment
And we want to reduce this window if the selected employee is not fully available
So we can guarantee our employees’ availability for booking

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer sees a 7-day window for appointment
Then the customer should see a 7-day window for appointment, including the current day

@valid @feature-example @regression-test
Scenario: Customer sees a reduced window for appointment
Given the selected employee is not fully available
Then the customer should see a reduced window for appointment

@invalid @feature-example @regression-test
Scenario: Customer sees an incorrect window for appointment
Given the selected employee is not fully available
When the customer views the available days for appointment
Then the customer should not see a 7-day window for appointment
Then the customer should see an error message

# The feature file can be named as "appointment_days.feature". The feature describes the ability of the business to present the customer with a 7-day window for appointment and reduce the window if the selected employee is not fully available. The background sets the context that the customer is on the booking page. The first scenario covers the positive scenario where the customer sees a 7-day window for appointment. The second scenario covers the positive scenario where the customer sees a reduced window for appointment if the selected employee is not fully available. The third scenario covers the negative scenario where the customer sees an incorrect window for appointment and sees an error message. The scenarios are tagged appropriately for easy filtering and execution.