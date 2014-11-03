using System.Net.Http;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
