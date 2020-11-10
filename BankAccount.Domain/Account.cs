using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public Bank Bank { get; set; }
        public int Balance { get; set; }
    }
}
