Feature: Bank account management 
  In order to user a created bank account
  As a user
  I want the system to be able to create an account, let me make operations on my account,
  show me my account statementn and give me access to the account history (statement printing)
 
 Background: Account creation
  Given a created user named Alin 
  When Alin create the account with a balance of 32
  Then Alin should have a new accounct with balance 32

  Scenario: Deposit into account
  Given a created account by Alin
  When Alin make a deposit of 32
  Then Alin should have a balance 64 in his account

  Scenario: Withdrawal 10 from an account with a balance of 32
  Given a created account by Alin containing 32
  When Alin make a withdrawal of 10
  Then Alin should have a balance of 22 in his account

