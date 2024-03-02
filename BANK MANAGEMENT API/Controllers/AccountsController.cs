using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Managers;
using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BANK_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsManager _accountsManager;

        public AccountsController(IAccountsManager accountsManager)
        {
            _accountsManager = accountsManager;
        }

        //get all accounts details...
        [HttpGet("All")]
        //api/Accounts/All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _accountsManager.GetAllAccounts();
            return accounts == null ? StatusCode(500, "Internal server error") : Ok(accounts);
        }

        //Get account by account no...
        [HttpGet("{accountNo:Decimal}")]
        //api/Accounts/555577778888
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Account>> GetAccountByAccountNo([FromRoute] Decimal accountNo)
        {
            var account = await _accountsManager.GetAccount(accountNo);
            if (account != null)
            {
                return Ok(account);
            }
            else if (account == null)
            {
                return NotFound("No account found with given account no.");
            }
            return StatusCode(500, "Internal server error");
        }

        //update account status...
        [HttpPut("Update/{accountNo:Decimal}")]
        //api/Accounts/Update/333344445555
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCustomer([FromRoute] Decimal accountNo, [FromBody] Boolean newStatus)
        {
            int status = await _accountsManager.UpdateAccountStatus(accountNo, newStatus);
            switch (status)
            {
                case 0: return Ok("Account status updated successfully...");
                case 1: return NotFound("Account not found...");
                case 2: return BadRequest("Account status is already desired of your choice...");
            }
            return StatusCode(500, "Internal server error");
        }


        //delete account with account id...
        [HttpDelete("Delete/{accountNo:Decimal}")]
        //api/Accounts/Delete/222233334444
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAccount([FromRoute] Decimal accountNo)
        {
            int status = await _accountsManager.DeleteAccount(accountNo);
            switch (status)
            {
                case 0: return Ok("Account deleted successfully...");
                case 1: return NotFound("Account not found...");
                case 2: return BadRequest("Data not provided in proper format...");
            }
            return StatusCode(500, "Internal server error");
        }

        //add new customer account...
        [HttpPost("Add")]
        //api/Accounts/Add
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAccount([FromBody] AddAccountDTO newAccountData)
        {
            Decimal accountNo = await _accountsManager.AddAccount(newAccountData);
            if (accountNo == 0) return BadRequest("Something went wrong...");
            return Ok($"Account created successfully ... \n Account No:{accountNo}");
        }


    }
}
