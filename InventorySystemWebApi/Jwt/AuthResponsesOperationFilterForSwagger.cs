using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InventorySystemWebApi.Jwt
{
    public class AuthResponsesOperationFilterForSwagger : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true).Union(context.MethodInfo.GetCustomAttributes(true));

            // If there is [AllowAnonymous] attribute on controller method.
            if (attributes!.OfType<IAllowAnonymous>().Any())
            {
                return;
            }

            var authAttributes = attributes!.OfType<IAuthorizeData>();

            // If there is any authorize attribute on controller method.
            if (authAttributes.Any())
            {
                operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };

                if (authAttributes.Any(att => !String.IsNullOrWhiteSpace(att.Roles) || !String.IsNullOrWhiteSpace(att.Policy)))
                {
                    operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };
                }

                // Adding the Security Requirement.
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "BearerAuth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },

                            Array.Empty<string>()
                        }
                    }
                };
            }
        }
    }
}
