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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsManager _transactionsManager;

        public TransactionsController(ITransactionsManager transactionsManager)
        {
            _transactionsManager = transactionsManager;
        }

        //get all transactions details...
        [HttpGet("All")]
        //api/Transactions/All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _transactionsManager.GetAllTransactions();
            return transactions == null ? StatusCode(500, "Internal server error") : Ok(transactions);
        }

        //Get transaction by transaction id...
        [HttpGet("{transactionId:int}")]
        //api/Transactions/12
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transaction>> GetTransactionByTransactionId([FromRoute] int transactionId)
        {
            var transaction = await _transactionsManager.GetTransactionById(transactionId);
            if (transaction != null)
            {
                return Ok(transaction);
            }
            else if (transaction == null)
            {
                return NotFound("No transaction found with given transaction id");
            }
            return StatusCode(500, "Internal server error");
        }

        //Get transaction by account no...
        [HttpGet("Account/{accountNo:Decimal}")]
        //api/Transactions/Account/223344556677
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsByAccountNo([FromRoute] Decimal accountNo)
        {
            var transactions = await _transactionsManager.GetTransactionsByAccountNo(accountNo);
            if (transactions != null)
            {
                return Ok(transactions);
            }
            else if (transactions == null)
            {
                return NotFound("No transaction found with given transaction id");
            }
            return StatusCode(500, "Internal server error");
        }

        //perform new transaction...
        [HttpPost("Add")]
        //api/Transactions/Add
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> PerformTransaction([FromBody] PerformTransactionDTO transactionData)
        {
            int status =await _transactionsManager.PerformTransaction(transactionData);

            switch (status)
            {
                case 0:return Ok("Transaction successfully completed...");
                case 1:return BadRequest("Insuffient Balance");
                case 2:return NotFound("Account Not found");
                case 4:return StatusCode(403,"Account is Inactive");
            }

            return StatusCode(500, "Something went wrong...");
        }

    }
}
