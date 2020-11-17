using BankTests.Drivers;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace BankTests.StepDefinitions
{
    [Binding]
    public class BankAccountManagementSteps
    {
        private readonly BankDriver _driver;

        public BankAccountManagementSteps(BankDriver driver)
        {
            _driver = driver;
        }
        [Given(@"a created user named (.*)")]
        public void GivenACreatedUserNamedAlin(string userName)
        {
            _driver.Seed(userName: userName);
        }
        
        [When(@"(.*) create the account")]
        public async void WhenAlinCreateTheAccount(string userName)
        {
            var user = await _driver.GetUserByName(userName);
            await _driver.CreateAccount(user.Bank, userName, 0);
        }
        
        [Then(@"(.*) should have a new accounct with balance (\d*)")]
        public async void ThenAlinShouldHaveANewAccounctWithBalance(string userName, int balance)
        {
            var user = await _driver.GetUserByName(userName);
            var account = await _driver.GetAccountByUser(user) ;
            account.Balance.Should().Be(balance);
        }
    }
}
