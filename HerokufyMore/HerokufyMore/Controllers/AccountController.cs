using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerokufyMore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HerokufyMore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Logins a user
        /// </summary>
        /// <param name="login">A login information</param>
        /// <returns>Response code</returns>
        [HttpPost, Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (result.Succeeded)
                return Ok();

            return BadRequest("Invalid Login Attempt");
        }
    }
}
