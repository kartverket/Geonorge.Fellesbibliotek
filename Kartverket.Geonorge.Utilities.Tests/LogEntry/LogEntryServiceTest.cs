using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kartverket.Geonorge.Utilities.LogEntry;
using Kartverket.Geonorge.Utilities.Organization;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace Kartverket.Geonorge.Utilities.Tests.LogEntry
{
    public class LogEntryServiceTest
    {

        [Fact]
        public async Task ShouldReturnSuccessfullyWhenAddingToLog()
        {
            string logUrl = "http://api.dev.geonorge.no/endringslogg/";
            Utilities.LogEntry.LogEntry log = new Utilities.LogEntry.LogEntry
            {
                Application = "Register",
                User = "demobruker",
                Description = "testing logging",
                ElementId = "83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa"
            };

            const string content =
             @"{Application:""Register"", User:""demobruker"", Description:""testing logging"",ElementId : ""83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa"" }";
            var httpClient = CreateHttpClientFactory(HttpStatusCode.NoContent, content);

            LogEntryService service = new LogEntryService(logUrl, httpClient);
            HttpStatusCode actualStatusCode = await service.AddLogEntry(log);
            var expectedStatusCode = HttpStatusCode.NoContent;
            Assert.Equal(expectedStatusCode, actualStatusCode);

        }



        //[Fact]
        //public async Task ShouldReturnSuccessfullyWhenAddingLiveToLog()
        //{
        //    string logUrl = "http://api.dev.geonorge.no/endringslogg/";
        //    Utilities.LogEntry.LogEntry log = new Utilities.LogEntry.LogEntry
        //    {
        //        Application = "Register",
        //        User = "demobruker",
        //        Description = "testing logging",
        //        ElementId = "83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa"
        //    };

        //    LogEntryService service = new LogEntryService(logUrl, new HttpClientFactory());
        //    HttpStatusCode actualStatusCode = await service.AddLogEntry(log);
        //    var expectedStatusCode = HttpStatusCode.NoContent;
        //    Assert.Equal(expectedStatusCode, actualStatusCode);

        //}


        [Fact]
        public void ShouldReturnOneLogEntryWhenGetEntriesForElement()
        {
            string elementId = "83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa";

            Utilities.LogEntry.LogEntry log = new Utilities.LogEntry.LogEntry
            {
                Application = "Register",
                User = "demobruker",
                Description = "testing logging",
                ElementId = elementId
            };

            List<Utilities.LogEntry.LogEntry> logEntries = new List<Utilities.LogEntry.LogEntry>();
            logEntries.Add(log);

            var mockService = new Mock<ILogEntryService>();
            mockService.Setup(l => l.GetEntriesForElement(elementId, 1)).Returns(Task.FromResult(logEntries));

            mockService.Object.GetEntriesForElement(elementId, 1).Result.Should().HaveCount(1);

        }

        //[Fact]
        //public async Task ShouldReturnOneLogEntryWhenFoundLive()
        //{
        //    string logUrl = "http://api.dev.geonorge.no/endringslogg/";
        //    string elementId = "83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa";

        //    LogEntryService service = new LogEntryService(logUrl, new HttpClientFactory());
        //    List<Utilities.LogEntry.LogEntry> logs = await service.GetEntriesForElement(elementId, 1);

        //    logs.Should().HaveCount(1);
        //}

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
