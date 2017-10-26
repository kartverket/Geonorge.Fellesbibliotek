using System.Net.Http;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
