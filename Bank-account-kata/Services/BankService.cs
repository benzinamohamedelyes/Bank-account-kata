using BankAccount.Data;
using BankAccount.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank_account_kata.Services
{
    public class BankService
    {
        private readonly BankAccountContext _context;

        public BankService(BankAccountContext context)
        {
            _context = context;
        }
        internal async Task<Bank> GetBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            return bank;
        }
    }
}
