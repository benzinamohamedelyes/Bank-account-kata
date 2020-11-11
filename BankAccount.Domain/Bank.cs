using System;
using System.Collections.Generic;

namespace BankAccount.Domain
{
    public class Bank
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public ICollection<User> Clients { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
