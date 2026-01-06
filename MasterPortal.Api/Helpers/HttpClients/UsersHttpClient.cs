using MasterPortal.Api.Helpers.HttpClients.Base;

namespace MasterPortal.Api.Helpers.HttpClients
{
    public class UsersHttpClient(HttpClient httpClient) : DefaultHttpClient(httpClient)
    {
    }
}
