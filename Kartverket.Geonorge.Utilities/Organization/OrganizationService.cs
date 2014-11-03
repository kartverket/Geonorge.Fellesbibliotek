using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly string _registryUrl;
        private readonly IHttpClientFactory _factory;

        public OrganizationService(string registryUrl, IHttpClientFactory factory)
        {
            _registryUrl = registryUrl;
            _factory = factory;
        }

        public async Task<Organization> GetOrganizationByName(string name)
        {
            HttpClient client = _factory.GetHttpClient();

            client.BaseAddress = new Uri(_registryUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/organisasjon/navn/" + name).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                Organization organization = await response.Content.ReadAsAsync<Organization>().ConfigureAwait(false);
                return organization;
            }
            return null;
        }
    }

    
}
