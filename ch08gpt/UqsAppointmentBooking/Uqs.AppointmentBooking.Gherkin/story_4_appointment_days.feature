Feature: Appointment Booking
  As a business
  I want to present a 7-day window for appointment selection
  So that I can guarantee employee availability

  @feature-example
  Scenario Outline: Select appointment day
    Given the following employees are available:
      | Name      |
      | Barber 1  |
      | Barber 2  |
      | Barber 3  |
    And "<employee>" is available on the following days:
      | Day        | Available |
      | 2022-04-03 | Yes       |
      | 2022-04-04 | No        |
      | 2022-04-05 | Yes       |
      | 2022-04-06 | Yes       |
      | 2022-04-07 | Yes       |
      | 2022-04-08 | Yes       |
      | 2022-04-09 | Yes       |
    When I select "<employee>" as the employee
    Then I should see the following available days:
      | Day        |
      | 2022-04-03 |
      | 2022-04-05 |
      | 2022-04-06 |
      | 2022-04-07 |
      | 2022-04-08 |
      | 2022-04-09 |

    Examples:
      | employee  |
      | Any       |
      | Barber 1  |
      | Barber 2  |
      | Barber 3  |

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