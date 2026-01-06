using MasterPortal.Api.HttpClients.Base;

namespace MasterPortal.Api.HttpClients
{
    public class NotificationsHttpClient(HttpClient httpClient) : DefaultHttpClient(httpClient)
    {
    }
}
