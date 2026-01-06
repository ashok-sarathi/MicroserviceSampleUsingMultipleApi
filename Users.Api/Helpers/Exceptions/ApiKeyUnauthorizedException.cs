namespace Users.Api.Helpers.Exceptions
{
    public class ApiKeyUnauthorizedException(string message) : Exception(message)
    {
    }
}
