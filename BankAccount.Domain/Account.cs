using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankAccount.Domain
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public Bank Bank { get; set; }
        public int Balance { get; set; }
    }
}
