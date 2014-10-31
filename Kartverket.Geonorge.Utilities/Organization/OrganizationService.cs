using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.Organization
{
    public class OrganizationService : IOrganizationService
    {
        private readonly string _registryUrl;
        private readonly HttpClient _httpClient;

        public OrganizationService(string registryUrl) : this(registryUrl, new HttpClient()) { }

        public OrganizationService(string registryUrl, HttpClient httpClient)
        {
            _registryUrl = registryUrl;
            _httpClient = httpClient;
        }

        public async Task<Organization> GetOrganizationByName(string name)
        {
            _httpClient.BaseAddress = new Uri(_registryUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync("api/organisasjon/navn/" + name);
            if (response.IsSuccessStatusCode)
            {
                Organization organization = await response.Content.ReadAsAsync<Organization>();
                return organization;
            }
            return null;
        }
    }

    
}
