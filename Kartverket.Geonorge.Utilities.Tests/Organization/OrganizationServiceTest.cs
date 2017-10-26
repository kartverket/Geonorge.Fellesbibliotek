using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kartverket.Geonorge.Utilities.Organization;
using Xunit;

namespace Kartverket.Geonorge.Utilities.Tests.Organization
{
    public class OrganizationServiceTest
    {
        public OrganizationServiceTest()
        {
            log4net.Config.BasicConfigurator.Configure();

        }

        [Fact]
        public void ShouldReturnOrganizationWhenFoundByName()
        {
            const string registryUrl = "http://dummy";
            const string content =
                @"{Number:""123456789"", Name:""Kartverket"", LogoUrl:""http://example.com/logo.png""}";

            var httpClient = CreateHttpClientFactory(HttpStatusCode.OK, content);

            var service = new OrganizationService(registryUrl, httpClient);
            Task<Utilities.Organization.Organization> task = service.GetOrganizationByName("Kartverket");
            Utilities.Organization.Organization organization = task.Result;

            organization.Should().NotBeNull();
            organization.Number.Should().Be("123456789");
            organization.Name.Should().Be("Kartverket");
            organization.LogoUrl.Should().Be("http://example.com/logo.png");
        }

        [Fact]
        public void ShouldReturnNullWhenNoContentFound()
        {
            const string registryUrl = "http://dummy";
            const string content = "";

            var httpClient = CreateHttpClientFactory(HttpStatusCode.NotFound, content);

            var service = new OrganizationService(registryUrl, httpClient);
            Task<Utilities.Organization.Organization> task = service.GetOrganizationByName("Kartverket");
            Utilities.Organization.Organization organization = task.Result;

            organization.Should().BeNull();
        }

        private IHttpClientFactory CreateHttpClientFactory(HttpStatusCode httpStatusCode, string content)
        {
            var response = new HttpResponseMessage(httpStatusCode);
            response.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient(new FakeHttpHandler
            {
                Response = response,
                InnerHandler = new HttpClientHandler()
            });

            return new FakeHttpClientFactory(httpClient);
        }
    }

    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _client;

        public FakeHttpClientFactory(HttpClient client)
        {
            _client = client;
        }

        public HttpClient GetHttpClient()
        {
            return _client;
        }
    }


    public class FakeHttpHandler : DelegatingHandler
    {
        public HttpResponseMessage Response { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken)
        {
            if (Response == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            return Task.Factory.StartNew(() => Response);
        }
    }
}

