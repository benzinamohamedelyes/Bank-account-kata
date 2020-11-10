using Bank_account_kata.Controllers;
using Bank_account_kata.Services;
using BankAccount.Data;
using BankAccount.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BankTests
{
    public class InMemoryTests
    {
        [Fact]
        public void InitializeInitializeShouldHaveGenerateData()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureCreated();
                DataGenerator.Seed(context);
                var bank = context.Banks.FirstOrDefault();
                bank.Should().NotBeNull();
                var user = context.Users.FirstOrDefault();
                user.Should().NotBeNull();
            }
        }
        [Fact]
        public async Task PostAccount_ReturnsIActionResult_WithAnAccount()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureCreated();
                var controller = new AccountsController(new AccountService(context));
                DataGenerator.Seed(context);
                var bank = context.Banks.FirstOrDefault();
                bank.Should().NotBeNull();
                var user = context.Users.FirstOrDefault();
                user.Should().NotBeNull();

                Account newAccount = new Account();
                
                var result = await controller.PostAccount(newAccount);

                result.Should().BeOfType<BadRequestResult>();

                newAccount = new Account()
                {
                    Bank = bank,
                    Owner = user,
                    Balance = 0
                };

                result = await controller.PostAccount(newAccount);
                var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
                var account = createdResult.Value.Should().BeAssignableTo<Account>().Subject;

                account.Owner.Should().Be(newAccount.Owner);
                account.Bank.Should().Be(newAccount.Bank);
                account.Balance.Should().Be(newAccount.Balance);
            }
        }
    }
}
