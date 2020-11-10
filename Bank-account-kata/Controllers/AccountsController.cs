using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccount.Data;
using BankAccount.Domain;
using Bank_account_kata.Services;

namespace Bank_account_kata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            try
            {
                if (await _accountService.CreateAccount(account))

                    return CreatedAtAction("GetAccount", new { id = account.Id }, account);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
