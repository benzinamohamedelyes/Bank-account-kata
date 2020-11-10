using System;
using System.Collections.Generic;

namespace BankAccount.Domain
{
    public class Bank
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public IEnumerable<User> Clients { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
