# Bank account kata [![Build Status](https://travis-ci.com/benzinamohamedelyes/Bank-account-kata.svg?token=9Ly8gC3zrm9pNgP1snsx&branch=main)](https://travis-ci.com/benzinamohamedelyes/Bank-account-kata)
Think of your personal bank account experience When in doubt, go for the simplest solution

# Requirements
- Account creation
- Deposit and Withdrawal
- Account statement (date, amount, balance)
- Statement printing
 
# User Stories
##### US 1:
**In order to** have an account  
**As a** bank client  
**I want to** create an account 

##### US 2:
**In order to** save money  
**As a** bank client  
**I want to** make a deposit in my account  
 
##### US 3: 
**In order to** retrieve some or all of my savings  
**As a** bank client  
**I want to** make a withdrawal from my account  
 
##### US 4: 
**In order to** check my operations  
**As a** bank client  
**I want to** see the history (operation, date, amount, balance)  of my operations  

##### US 5: 
**In order to** been denied the retrieval of my money
**As a** bank client  
**I want to** make a withdrawal from my account (withdrawal amount greater than my balance)

# IMPLEMENTATION

## Technical stack
In order to complete this kata, I choose the following stack:
- Language: `C#`, `Typescript`
- Framework: `.Net Core`, `Angular`
- ORM: `Entity Framework Core`
- Unit test framework: `XUnit`
- Fonctional test framework: `SpecFlow`

## Conception

Basically, the program will an Angular application embedded in a .Net Core application:
- A .Net Core MVC application with Entity Framework Core's InMemory Provider to create a working "database" in memory
- An Angular application for the front-end Part (located under ClientApp folder)
# RUN

**Build the Project**
```shell
donet build
```

**Launch the program**
```shell
donet run 
```
```shell
http://localhost:5000/
```
