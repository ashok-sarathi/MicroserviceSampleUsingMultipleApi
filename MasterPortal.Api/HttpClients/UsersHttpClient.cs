using MasterPortal.Api.HttpClients.Base;

namespace MasterPortal.Api.HttpClients
{
    public class UsersHttpClient(HttpClient httpClient) : DefaultHttpClient(httpClient)
    {
    }
}
