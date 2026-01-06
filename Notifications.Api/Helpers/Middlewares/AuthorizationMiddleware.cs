namespace Notifications.Api.Helpers.Middlewares
{
    public class AuthorizationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                context.Items["ApiKey"] = extractedApiKey.ToString();
            }
            await next(context);
        }
    }
}
