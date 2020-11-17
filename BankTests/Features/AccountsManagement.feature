Feature: Bank account management 
  In order to user a created bank account
  As a user
  I want the system to be able to create an account, let me make operations on my account,
  show me my account statementn and give me access to the account history (statement printing)
  
Scenario: Account creation
  Given a created user named Alin
  When Alin create the account
  Then Alin should have a new accounct with balance 0