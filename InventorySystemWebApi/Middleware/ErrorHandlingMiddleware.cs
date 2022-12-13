using InventorySystemWebApi.Exceptions;

namespace InventorySystemWebApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException exception)
			{
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(exception.Message);
            }
        }
    }
}
