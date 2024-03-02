using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Managers;
using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BANK_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersManager _customersManager;
        public CustomersController(ICustomersManager customersManager)
        {
            _customersManager = customersManager;
        }

        //get all customers details...
        [HttpGet("All")]
        //api/Customers/All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers=await _customersManager.GetCustomers();
            return customers == null ? StatusCode(500,"Internal server error"):Ok(customers);
        }


        //Get customer by aadhar no...
        [HttpGet("{aadharNo}")]
        //api/Customers/555577778888
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> GetCustomerByAadharNo([FromRoute] string aadharNo)
        {
            var customer = await _customersManager.GetCustomer(aadharNo);
            if (customer != null)
            {
                return Ok(customer);
            }
            else if(customer == null)
            {
                return NotFound("No customer found with given aadhar no.");
            }
            return StatusCode(500, "Internal server error");
        }


        //update customer details...
        [HttpPut("Update/{customerId:int}")]
        //api/Customers/Update/3
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCustomer([FromRoute] int customerId, [FromBody] CustomerUpdateDTO customerDTO)
        {
            int status=await _customersManager.UpdateCustomer(customerId, customerDTO);
            switch(status)
            {
                case 0:return Ok("Customer information updated successfully...");
                case 1:return NotFound("Customer not found...");
                case 2:return BadRequest("Data not provided in proper format...");
            }
            return StatusCode(500, "Internal server error");
        } 
        

        //delete customer with customer id...
        [HttpDelete("Delete/{customerId:int}")]
        //api/Customers/Delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            int status=await _customersManager.DeleteCustomer(customerId);
            switch(status)
            {
                case 0:return Ok("Customer information deleted successfully...");
                case 1:return NotFound("Customer not found...");
                case 2:return BadRequest("Data not provided in proper format...");
            }
            return StatusCode(500, "Internal server error");
        }

        //Get Accounts by customer id...
        [HttpGet("Accounts/{customerId:int}")]
        //api/Customers/55
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AccountsListDTO>>> GetAccountsByCustomerId([FromRoute] int customerId)
        {
            var accounts = await _customersManager.GetAccountsByCustomerId(customerId);
            if (accounts != null)
            {
                return Ok(accounts);
            }
            else if (accounts == null)
            {
                return NotFound("No customer found with given aadhar no.");
            }
            return StatusCode(500, "Internal server error");
        }


        //get all customers details with similar name...
        [HttpGet("All/{name}")]
        //api/Customers/All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersWithMatchingName([FromRoute] string name)
        {
            var customers = await _customersManager.GetCustomersByName(name);
            return customers == null ? StatusCode(404, "No customers found with given name") : Ok(customers);
        }


        //Get customer by account no...
        [HttpGet("Account/{accountNo:Decimal}")]
        //api/Customers/555577778888
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> GetCustomerByAccountNo([FromRoute] Decimal accountNo)
        {
            var customer = await _customersManager.GetCustomerByAccountNo(accountNo);
            if (customer != null)
            {
                return Ok(customer);
            }
            else if (customer == null)
            {
                return NotFound("Account Not found...");
            }
            return StatusCode(500, "Internal server error");
        }
    }
}
