using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccount.Data;
using BankAccount.Domain;

namespace Bank_account_kata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankAccountContext _context;

        public AccountsController(BankAccountContext context)
        {
            _context = context;
        }
        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
