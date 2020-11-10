using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Domain
{
    public class AccountHistory
    {
        public int Id { get; set; }
        public Operation Operation { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }

    }
}
