using Microsoft.AspNetCore.Mvc.Filters;
using Users.Api.Helpers.Exceptions;

namespace Users.Api.Helpers.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiKeyAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var expectedApiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new NullReferenceException("API_KEY configuration is missing");

            if (context.HttpContext.Items.TryGetValue("ApiKey", out var apiKeyObj))
            {
                var apiKey = apiKeyObj as string;

                if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
                {
                    throw new ApiKeyUnauthorizedException("Unauthorized: Invalid API Key");
                }
            }
            else
            {
                throw new ApiKeyUnauthorizedException("Unauthorized: API Key is missing");
            }
        }
    }
}
