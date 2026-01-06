namespace MasterPortal.Api.Helpers.HttpClients.Base
{
    public abstract class DefaultHttpClient(HttpClient httpClient)
    {
        public virtual Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string route, Dictionary<string, string>? headers = null, dynamic? content = null)
        {
            var request = new HttpRequestMessage(httpMethod, route);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (content != null)
            {
                request.Content = JsonContent.Create(content);
            }

            return httpClient.SendAsync(request);
        }
    }
}
