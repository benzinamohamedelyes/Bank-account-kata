using Bank_account_kata.ViewModels;
using BankAccount.Domain;
using BankTests.Drivers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
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
        [Given(@"a created user named (\D+)")]
        public void GivenACreatedUserNamed(string userName)
        {
            _driver.Seed(userName: userName);
        }

        [When(@"(\D+) create the account with a balance of (\d+)")]
        public async void WhenUserCreateTheAccount(string userName, int balance)
        {
            var user = await _driver.GetUserByName(userName);
            await _driver.CreateAccount(user.Bank, userName, balance);
        }

        [Then(@"(\D+) should have a new accounct with balance (\d+)")]
        public async void ThenUserShouldHaveANewAccounctWithBalance(string userName, int balance)
        {
            var account = await GetAccountByUserName(userName);
            account.Balance.Should().Be(balance);
        }

        [Given(@"a created account by (\D+)")]
        public async void GivenACreatedAccountByUser(string userName)
        {
            var account = await GetAccountByUserName(userName);
            account.Should().NotBeNull();
            account.Balance.Should().Be(32);
        }

        [When(@"(\D+) make a deposit of (\d+)")]
        public async void WhenUserMakeADepositOf(string userName, int amount)
        {
            var account = await GetAccountByUserName(userName);
            var transcation = new TransactionViewModel()
            {
                Amount = amount,
                Operation = Operation.Deposit
            };
            var depositResult = await _driver.MakeTransaction(account.Id, transcation);
            depositResult.Should().BeTrue();
        }

        [Then(@"(\D+) should have a balance (\d+) in his account")]
        public async void ThenUseruldHaveABalanceInHisAccount(string userName, int amount)
        {
            var account = await GetAccountByUserName(userName);
            account.Should().NotBeNull();
            account.Balance.Should().Be(amount);
        }

        [Given(@"a created account by (\D+) containing (\d+)")]
        public async void GivenACreatedAccountByUserWithPositiveBalance(string userName, int balance)
        {
            var account = await GetAccountByUserName(userName);
            account.Should().NotBeNull();
            account.Balance.Should().Be(balance);
        }

        [When(@"(\D+) make a withdrawal of (\d+)")]
        public async void WhenUserMakeAWithdrawlOf(string userName, int amount)
        {
            var account = await GetAccountByUserName(userName);
            var formerAccountBalance = account.Balance;
            var transcation = new TransactionViewModel()
            {
                Amount = amount,
                Operation = Operation.Withdrawal
            };
            var depositResult = await _driver.MakeTransaction(account.Id, transcation);

            if (formerAccountBalance - amount > 0)
                depositResult.Should().BeTrue();
            else
                depositResult.Should().BeFalse();
        }

        [Then(@"(\D+) should have a balance of (\d+) in his account")]
        public async void ThenUserShouldHaveABalanceOfInHisAccount(string userName, int amount)
        {
            var account = await GetAccountByUserName(userName);
            account.Should().NotBeNull();
            account.Balance.Should().Be(amount);
        }

        
        private async Task<Account> GetAccountByUserName(string userName)
        {
            var user = await _driver.GetUserByName(userName);
            return await _driver.GetAccountByUser(user);
        }
    }
}
