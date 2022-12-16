using AutoMapper;
using Database;
using Database.Entities.Item;
using Database.Entities.User;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Jwt;
using InventorySystemWebApi.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventorySystemWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly InventorySystemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtConfig _jwtConfig;

        public AccountService(InventorySystemDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<string> LoginRequest(LoginRequestDto dto)
        {
            // Get user by e-mail address.
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
            {
                // Custom exception (used middleware).
                throw new BadRequestException("Invalid username or password.");
            }

            // Verify password (AspNetCore Identity).
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Custom exception (used middleware).
                throw new BadRequestException("Invalid username or password.");
            }

            // JWT - create token.
            var jwt = GenerateJwtToken(user);

            return jwt;
        }

        // JWT - create token.
        protected string GenerateJwtToken(Database.Entities.User.User user)
        {
            // Secret used to sign and verify JWT tokens.
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            // Information which used to create a security token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),

                // Generate token that is valid for the set time.
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpireHours),

                // Cryptographic key and security algorithms that are used to generate a digital signature.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token.
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
