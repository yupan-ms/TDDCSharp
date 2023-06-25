# gpt-4
Feature: Random Selection with Any Employee
As a business
When [Any employee] is selected
And more than one employee is free at the selected slot
And I hit Book
A free employee is selected randomly
So I ensure our employees are allocated to appointments fairly

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer selects [Any employee] and more than one employee is free at the selected slot
Given the customer selects [Any employee]
And more than one employee is free at the selected slot
When the customer hits the Book button
Then a free employee should be selected randomly

# The feature file can be named as "random_selection_with_any_employee.feature". The feature describes the ability of the business to randomly select a free employee when [Any employee] is selected and more than one employee is free at the selected slot. The background sets the context that the customer is on the booking page. The scenario covers the positive scenario where the customer selects [Any employee] and more than one employee is free at the selected slot, and a free employee is selected randomly when the customer hits the Book button. The scenario is tagged appropriately for easy filtering and execution.

