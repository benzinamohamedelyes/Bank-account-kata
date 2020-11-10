﻿using BankAccount.Data;
using BankAccount.Domain;
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
    }
}
