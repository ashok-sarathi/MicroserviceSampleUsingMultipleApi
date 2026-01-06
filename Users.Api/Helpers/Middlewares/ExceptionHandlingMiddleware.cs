using System.Net;
using System.Text.Json;
using Users.Api.Helpers.Exceptions;

namespace Users.Api.Helpers.Middlewares
{
    public class ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    ApiKeyUnauthorizedException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError
                };

                var response = new
                {
                    status = (int)statusCode,
                    message = ex.Message,
                    traceId = context.TraceIdentifier
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;


                logger.LogError(ex, $"Exception: {ex.Message}");

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
