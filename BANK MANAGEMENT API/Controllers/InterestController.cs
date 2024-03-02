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
    public class InterestController : ControllerBase
    {
        private readonly IInterestManager _interestManager;

        public InterestController(IInterestManager interestManager)
        {
            _interestManager = interestManager;
        }


        //get all interest-type details...
        [HttpGet("All")]
        //api/Interest/All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Interest>>> GetInterests()
        {
            var interests = await _interestManager.getAllInterest();
            return interests == null ? StatusCode(500, "Internal server error") : Ok(interests);
        }
    }
}
