using BankAccount.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank_account_kata.ViewModels
{
    public class TransactionViewModel
    {
        public int Amount { get; set; }
        public Operation Operation { get; set; }
    }
}
