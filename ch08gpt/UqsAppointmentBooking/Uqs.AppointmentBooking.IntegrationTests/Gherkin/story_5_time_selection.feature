#gpt-4

Feature: Time Selection
As a business
I want to present the customer with the time slots available for the selected employee for the selected date
And take into consideration existing employee appointments and the employee’s shifts
And round up any appointment to the nearest 5 minutes
And take into consideration the rest time of 5 minutes between appointments
So I ensure the customer is selecting an employee that is already available

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer sees available time slots for selected employee and date
Given the customer has selected an employee and date
When the customer views the available time slots
Then the customer should see the time slots available for the selected employee for the selected date
And the time slots should take into consideration existing employee appointments and the employee’s shifts
And the time slots should be rounded up to the nearest 5 minutes
And the time slots should take into consideration the rest time of 5 minutes between appointments

@invalid @feature-example @regression-test
Scenario: Customer sees no available time slots for selected employee and date
Given the customer has selected an employee and date
When the customer views the available time slots
Then the customer should see no available time slots for the selected employee for the selected date
And the customer should see an error message

# The feature file can be named as "time_selection.feature". The feature describes the ability of the business to present the customer with the available time slots for the selected employee and date. The background sets the context that the customer is on the booking page. The first scenario covers the positive scenario where the customer sees the available time slots for the selected employee and date, taking into consideration existing employee appointments, employee shifts, rounding up to the nearest 5 minutes, and rest time between appointments. The second scenario covers the negative scenario where the customer sees no available time slots for the selected employee and date and sees an error message. The scenarios are tagged appropriately for easy filtering and execution.

