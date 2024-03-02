using BANK_MANAGEMENT_API.Managers;
using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Text;

namespace BANK_MANAGEMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        

        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }


        [HttpPost("Register")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Register([FromBody] UserRegistration userRequest)
        {
            int status = await _authManager.Register(userRequest);
            switch(status)
            {
                case 0:return Ok("User Registration Successfull...");
                case 1:return Conflict("Username already taken...");
            }
            return StatusCode(500, "Something went wrong...");
        }



        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] UserRegistration loginRequest)
        {
            var token = await _authManager.Login(loginRequest);
            if(token==null)return Unauthorized("Invalid Username or Password");

            return Ok(token);
        }


    }
}
