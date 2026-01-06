namespace Notifications.Api.Helpers.Exceptions
{
    public class ApiKeyUnauthorizedException(string message) : Exception(message)
    {
    }
}
