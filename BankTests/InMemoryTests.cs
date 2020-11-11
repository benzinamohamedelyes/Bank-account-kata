using Bank_account_kata.Controllers;
using Bank_account_kata.Services;
using Bank_account_kata.ViewModels;
using BankAccount.Data;
using BankAccount.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            builder.UseInMemoryDatabase("BankDataBase");
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
        public async Task GetBankById_ReturnsIActionResult_WithABank()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBase");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {

                context.Database.EnsureCreated();
                DataGenerator.Seed(context);
                var controller = new BanksController(new BankService(context));

                var result = await controller.GetBank(1);
                var bank = result.Value.Should().BeAssignableTo<Bank>().Subject;
                bank.BankName.Should().Be("The Awsome Bank");
                bank.Id.Should().Be(1);

                result = await controller.GetBank(44);
                result.Result.Should().BeOfType<NotFoundResult>();

            }
        }
        [Fact]
        public async Task GetUserById_ReturnsIActionResult_WithAUser()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBase");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {

                context.Database.EnsureCreated();
                DataGenerator.Seed(context);
                var controller = new UsersController(new UserService(context));

                var result = await controller.GetUser(1);
                var user = result.Value.Should().BeAssignableTo<User>().Subject;
                user.Name.Should().Be("Partik");
                user.Id.Should().Be(1);

                result = await controller.GetUser(44);
                result.Result.Should().BeOfType<NotFoundResult>();
            }
        }
        [Fact]
        public async Task PostAccount_ReturnsIActionResult_WithAnAccount()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBase");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureDeleted();
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
                    Balance = 3
                };

                result = await controller.PostAccount(newAccount);
                var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
                var account = createdResult.Value.Should().BeAssignableTo<Account>().Subject;

                account.Owner.Should().Be(newAccount.Owner);
                account.Bank.Should().Be(newAccount.Bank);
                account.Balance.Should().Be(newAccount.Balance);
            }
        }
        [Fact]
        public async Task GetAccounts_ReturnsIActionResult_WithAnAccount()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBaseTwo");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureCreated();
                var controller = new AccountsController(new AccountService(context));
                DataGenerator.Seed(context);
                var bank = context.Banks.FirstOrDefault();
                bank.Should().NotBeNull();
                var user = context.Users.FirstOrDefault();
                user.Should().NotBeNull();

                Account newAccount = new Account()
                {
                    Bank = bank,
                    Owner = user,
                    Balance = 120
                };

                await controller.PostAccount(newAccount);
                var result = await controller.GetAllAccounts();
                var listAccounts = result.Value.Should().BeAssignableTo<IEnumerable<Account>>().Subject;
                listAccounts.Should().HaveCount(1);
            }
        }
        [Fact]
        public async Task GetAccountById_ReturnsIActionResult_WithAnAccount()
        {
            await PostAccount_ReturnsIActionResult_WithAnAccount();
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBase");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureCreated();
                var controller = new AccountsController(new AccountService(context));

                var result = await controller.GetAccount(1);
                var account = result.Value.Should().BeAssignableTo<Account>().Subject;
                account.Balance.Should().Be(3);
                account.Id.Should().Be(1);

                result = await controller.GetAccount(44);
                result.Result.Should().BeOfType<NotFoundResult>();

            }
        }
        [Fact]
        public async Task PutAccount_MakeDepositAndWithdrawal()
        {
            await PostAccount_ReturnsIActionResult_WithAnAccount();
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BankDataBase");

            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                context.Database.EnsureCreated();
                var controller = new AccountsController(new AccountService(context));

                var result = await controller.GetAllAccounts();
                var listAccounts = result.Value.Should().BeAssignableTo<IEnumerable<Account>>().Subject;
                listAccounts.Should().HaveCount(1);
                var accountId = listAccounts.First().Id;

                var transcation = new TransactionViewModel()
                {
                    Amount = 300,
                    Operation = Operation.Deposit
                };
                var depositResult = await controller.PutAccount(accountId, transcation);
                depositResult.Should().BeOfType<AcceptedResult>();
                var getAccountResult = await controller.GetAccount(1);
                var account = getAccountResult.Value.Should().BeAssignableTo<Account>().Subject;
                account.Balance.Should().Be(303);

                transcation = new TransactionViewModel()
                {
                    Amount = 300,
                    Operation = Operation.Withdrawal
                };
                depositResult = await controller.PutAccount(accountId, transcation);
                depositResult.Should().BeOfType<AcceptedResult>();
                getAccountResult = await controller.GetAccount(1);
                account = getAccountResult.Value.Should().BeAssignableTo<Account>().Subject;
                account.Balance.Should().Be(3);

                depositResult = await controller.PutAccount(accountId, transcation);
                depositResult.Should().BeOfType<UnauthorizedResult>();
                getAccountResult = await controller.GetAccount(1);
                account = getAccountResult.Value.Should().BeAssignableTo<Account>().Subject;
                account.Balance.Should().Be(3);

                depositResult = await controller.PutAccount(5, transcation);
                depositResult.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
