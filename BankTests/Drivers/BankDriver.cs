using Bank_account_kata.Controllers;
using Bank_account_kata.Services;
using Bank_account_kata.ViewModels;
using BankAccount.Data;
using BankAccount.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankTests.Drivers
{
    public class BankDriver
    {
        private readonly BankAccountContext _context;

        public BankDriver()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("BDDBankDataBase");
            _context = new BankAccountContext(builder.Options);
        }
        public void Seed(string userName = "Patrik", string bankName = "The Awsome Bank")
        {
            DataGenerator.Seed(_context, userName, bankName);
        }
        public async Task<Bank> GetBankById(int id)
        {
            BanksController controller = new BanksController(new BankService(_context));

            ActionResult<Bank> result = await controller.GetBank(id);
            return result.Value;
        }
        public async Task<User> GetUserById(int id)
        {
            UsersController controller = new UsersController(new UserService(_context));

            ActionResult<User> result = await controller.GetUser(id);
            return result.Value;
        }
        public async Task<User> GetUserByName(string userName)
        {
            UserService userService = new UserService(_context);
            return await userService.GetUserByName(userName);
        }

        public async Task<Account> CreateAccount(Bank bank, string user, int balance)
        {
            AccountsController controller = new AccountsController(new AccountService(_context));
            UserService userService = new UserService(_context);
            User currentUser = await userService.GetUserByName(user);
            Account newAccount = new Account()
            {
                Bank = bank,
                Owner = currentUser,
                Balance = balance
            };

            IActionResult result = await controller.PostAccount(newAccount);
            if (typeof(CreatedAtActionResult).IsInstanceOfType(result))
            {
                CreatedAtActionResult actionResult = (CreatedAtActionResult)result;
                return (Account)actionResult.Value;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            AccountsController controller = new AccountsController(new AccountService(_context));

            ActionResult<IEnumerable<Account>> result = await controller.GetAllAccounts();

            return result.Value;
        }
        public async Task<Account> GetAccountByUser(User user)
        {
            AccountsController controller = new AccountsController(new AccountService(_context));

            ActionResult<Account> result = await controller.GetAccount(user);
            return result.Value;
        }
        public async Task<bool> MakeTransaction(int accountId, TransactionViewModel transaction)
        {
            var controller = new AccountsController(new AccountService(_context));


            var depositResult = await controller.MakeTransaction(accountId, transaction);
            if (typeof(AcceptedResult).IsInstanceOfType(depositResult))
                return true;
            else
                return false;
        }
    }
}
