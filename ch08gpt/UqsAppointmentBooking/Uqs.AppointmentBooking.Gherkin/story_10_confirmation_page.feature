# gpt-4
Feature: Confirmation Page
As a customer
I want to see that my appointment is booked
So I can rest assured that it is going ahead

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer sees confirmation of booked appointment
Given the customer has filled in all fields and selected a service, employee, date, and time
When the customer hits the Book button
Then the customer should see a confirmation page
And the confirmation page should show that the appointment is booked

# The feature file can be named as "confirmation_page.feature". The feature describes the ability of the customer to see a confirmation page after booking an appointment. The background sets the context that the customer is on the booking page. The scenario covers the positive scenario where the customer fills in all fields, selects a service, employee, date, and time, hits the Book button, and sees a confirmation page showing that the appointment is booked. The scenario is tagged appropriately for easy filtering and execution.