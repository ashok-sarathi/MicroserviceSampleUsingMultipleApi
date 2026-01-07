using MasterPortal.Api.Helpers.HttpClients.Base;

namespace MasterPortal.Api.Helpers.HttpClients
{
    public class DummyJsonClient(HttpClient httpClient) : DefaultHttpClient(httpClient)
    {
    }
}
