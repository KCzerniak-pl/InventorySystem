using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace InventorySystemWebApi.Tests.Integration
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Name, "Oliver Smith"),
                    new Claim(ClaimTypes.Email, "oliver.smith@test.com"),
                    new Claim(ClaimTypes.Role, "1")
                }));

            context.HttpContext.User = claimsPrincipal;

            await next();
        }
    }
}
