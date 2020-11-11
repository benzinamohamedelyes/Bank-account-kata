using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BankAccount.Domain
{
    public class Account
    {
        public Account()
        {
            AccountHistories = new List<AccountHistory>();
        }
        public int Id { get; set; }
        [Required]
        public User Owner { get; set; }
        public int UserId { get; set; }
        [Required]
        public Bank Bank { get; set; }
        public int Balance { get; set; }
        public ICollection<AccountHistory> AccountHistories { get; set; }
    }
}
