using BankAccount.Data;
using BankAccount.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank_account_kata.Services
{
    public class UserService
    {
        private readonly BankAccountContext _context;

        public UserService(BankAccountContext context)
        {
            _context = context;
        }
        internal async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Bank).FirstOrDefaultAsync(u=> u.Id == id);
            return user;
        }
        public async Task<User> GetUserByName(string name)
        {
            var user = await _context.Users.Include(u => u.Bank).FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }
    }
}
