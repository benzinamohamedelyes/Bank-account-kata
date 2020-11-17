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
using Bank_account_kata.ViewModels;

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
            catch 
            {
                return BadRequest();
            }

        }
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            return new ActionResult<IEnumerable<Account>>(await _accountService.GetAllAccounts());
        }
        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _accountService.GetAccount(id);

            if (account == null)
            {
                return NotFound();
            }

            return new ActionResult<Account>(account);

        }
        [HttpPost()]
        public async Task<ActionResult<Account>> GetAccount([FromBody]User user)
        {
            var account = await _accountService.GetAccount(user);

            if (account == null)
            {
                return NotFound();
            }

            return new ActionResult<Account>(account);

        }
        // PUT: api/Accounts/5
        [HttpPost("{id}")]
        public async Task<IActionResult> MakeTransaction(int id, [FromBody] TransactionViewModel transaction)
        {

            if (_accountService.AccountExists(id))
            {
                if (await _accountService.MakeOperationOnAccount(id, transaction.Operation, transaction.Amount))
                    return Accepted();
                else
                    return Unauthorized();
            }
            else
                return NotFound();
        }
    }
}
