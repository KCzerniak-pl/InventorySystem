using InventorySystemWebApi.Interfaces;
using InventorySystemWebApi.Models;
using InventorySystemWebApi.Models.Account;
using InventorySystemWebApi.Services;
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

        [HttpPost("create")]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountDto dto)
        {
            // Add new user.
            await _accountService.CreateAccount(dto);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> LoginRequest([FromBody] LoginRequestDto dto)
        {
            // Password verification and generate JWT.
            var jwt = await _accountService.LoginRequest(dto);

            return Ok(jwt);
        }
    }
}
