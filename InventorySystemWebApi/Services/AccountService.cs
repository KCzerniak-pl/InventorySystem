using AutoMapper;
using Database;
using Database.Entities.User;
using InventorySystemWebApi.Exceptions;
using InventorySystemWebApi.Interfaces;
using InventorySystemWebApi.Jwt;
using InventorySystemWebApi.Models.Account;
using Microsoft.AspNetCore.Identity;
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

        public async Task CreateAccount(CreateAccountDto dto)
        {
            // Check if email address already exists.
            var emailExists = await _dbContext.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists)
            {
                // Custom exception (to be caught by middleware).
                throw new BadRequestException("This email address is already registered.");
            }

            // Check if user role exists.
            var roleExists = await _dbContext.Roles.AnyAsync(r => r.Id == dto.RoleId);
            if (!roleExists)
            {
                // Custom exception (to be caught by middleware).
                throw new BadRequestException("This user role does not exist.");
            }

            // Map DTO to entity.
            var user = _mapper.Map<User>(dto);

            // Hash the password.
            var passwordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordHash;

            // Add new account.
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> LoginRequest(LoginRequestDto dto)
        {
            // Get user by email address.
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
            {
                // Custom exception (to be caught by middleware).
                throw new BadRequestException("Invalid username or password.");
            }

            // Verify password using AspNetCore Identity.
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Custom exception (to be caught by middleware).
                throw new BadRequestException("Invalid username or password.");
            }

            // Create JWT.
            var jwt = GenerateJwt(user);

            return jwt;
        }

        protected string GenerateJwt(Database.Entities.User.User user)
        {
            // Secret used to sign and verify JWT.
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
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.CreateToken(tokenDescriptor);
            var jwt = jwtHandler.WriteToken(token);

            return jwt;
        }
    }
}
