using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BankAccount.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Bank Bank { get; set; }
        public Account Account { get; set; }
        public int? AccountId { get; set; }
    }
}
