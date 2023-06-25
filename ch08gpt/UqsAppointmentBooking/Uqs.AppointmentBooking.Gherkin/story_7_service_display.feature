# gpt-4
Feature: Service Display
As a customer
I want a reminder of the name of the service that I picked, its price, and the required time
So I can review my selection before hitting the Book button

Background:
Given the customer is on the booking page

@valid @feature-example @smoke-test
Scenario: Customer sees a reminder of the service details
Given the customer has selected a service
Then the customer should see a reminder of the name of the service
And the customer should see a reminder of the price of the service
And the customer should see a reminder of the required time for the service

# The feature file can be named as "service_display.feature". The feature describes the ability of the customer to see a reminder of the service details before hitting the Book button. The background sets the context that the customer is on the booking page. The scenario covers the positive scenario where the customer sees a reminder of the name, price, and required time of the service they have selected. The scenario is tagged appropriately for easy filtering and execution.