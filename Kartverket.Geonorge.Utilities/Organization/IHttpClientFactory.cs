using System.Net.Http;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
