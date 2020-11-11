using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Bank Bank { get; set; }
        public Account Account { get; set; }
    }
}
