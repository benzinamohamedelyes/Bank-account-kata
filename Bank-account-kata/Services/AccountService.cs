using BankAccount.Data;
using BankAccount.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank_account_kata.Services
{
    public class AccountService
    {
        private readonly BankAccountContext _context;

        public AccountService(BankAccountContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAccount(Account account)
        {
            if (account.Bank != null && account.Owner != null)
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }



        }

        internal async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        internal async Task<Account> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account;
        }

        internal async Task<bool> MakeOperationOnAccount(int id, Operation operation, int amount)
        {
            var operationSuccess = true;
            var account = await GetAccount(id);
            var accountHistory = new AccountHistory()
            {
                Amount = amount,
                Date = DateTime.Now,
                Operation = operation
            };
            if (operation == Operation.Deposit)
            {
                account.Balance = account.Balance + amount;
                accountHistory.OperationStatus = OperationStatus.Approuved;
            }

            else
            {
                if ((account.Balance - amount) < 0)
                {
                    accountHistory.OperationStatus = OperationStatus.Denied;
                    operationSuccess = false;
                }
                else
                {
                    account.Balance = account.Balance - amount;
                    accountHistory.OperationStatus = OperationStatus.Approuved;
                }
            }

            account.AccountHistories.Add(accountHistory);
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return operationSuccess;
        }
        internal bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
