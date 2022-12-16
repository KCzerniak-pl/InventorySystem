using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InventorySystemWebApi.Jwt
{
    public static class JwtAuthenticationExtension
    {
        public static void AddAuthentication(IServiceCollection services)
        {
            // Get data from "JwtConfig" object.
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            JwtConfig jwtConfig = serviceProvider.GetService<IOptionsMonitor<JwtConfig>>()!.CurrentValue;

            // Validate token.
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Sets the default scheme to use when authenticating.   
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Sets the default scheme to use when challenging.   
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // Sets the default fallback scheme.

                })
                .AddJwtBearer(jwt =>
                {
                    // Secret used to sign and verify JWT tokens.
                    var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

                    // Token should be stored after a successful authorization.
                    jwt.SaveToken = true;

                    // Set of parameters that are used by validating the token.
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // Remove delay of token when expire.
                    };
                });
        }
    }
}
