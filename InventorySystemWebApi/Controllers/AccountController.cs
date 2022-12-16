using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InventorySystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginRequest([FromBody] LoginRequestDto loginRequest)
        {
            // Password verification and generate JWT.
            var jwt = await _accountService.LoginRequest(loginRequest);

            return Ok(jwt);
        }
    }
}
