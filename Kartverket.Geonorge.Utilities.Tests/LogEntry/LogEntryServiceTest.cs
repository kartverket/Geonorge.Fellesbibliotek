using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Kartverket.Geonorge.Utilities.LogEntry;
using Kartverket.Geonorge.Utilities.Organization;
using Xunit;

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
                User = "ESK_demobruker",
                Description = "testing logging",
                ElementId = "83f9cca1-8cf3-41c7-b55b-c80bbcca3dfa"
            };

            LogEntryService service = new LogEntryService(logUrl, new HttpClientFactory());
            HttpStatusCode actualStatusCode = await service.AddLogEntry(log);
            var expectedStatusCode = HttpStatusCode.NoContent;
            Assert.Equal(expectedStatusCode, actualStatusCode);

        }
    }
}
