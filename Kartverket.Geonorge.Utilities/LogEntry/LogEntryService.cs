using Kartverket.Geonorge.Utilities.Logging;
using Kartverket.Geonorge.Utilities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.LogEntry
{
    public class LogEntryService : ILogEntryService
    {
        private static readonly ILog Logger = LogProvider.For<LogEntryService>();

        private readonly string _logUrl;
        private readonly IHttpClientFactory _factory;

        public LogEntryService(string logUrl, IHttpClientFactory factory)
        {
            _logUrl = logUrl;
            _factory = factory;
        }
        public async Task<HttpStatusCode> AddLogEntry(LogEntry logEntry)
        {
            Logger.Debug(string.Format("Adding log entry: {0}", logEntry));

            HttpClient client = _factory.GetHttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(_logUrl);

            HttpResponseMessage response = await client.PostAsJsonAsync("api/logentry/add", logEntry).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsAsync<LogEntry>().ConfigureAwait(false);

                Logger.Debug(string.Format("LogEntry added. [Application={0}], [User={1}], [Description={2}], [ElementId={3}]"
                    , logEntry.Application, logEntry.User, logEntry.Description, logEntry.ElementId));

                return HttpStatusCode.NoContent;
            }

            Logger.Error(string.Format("LogEntry not added. [Application={0}], [User={1}], [Description={2}], [ElementId={3}]"
                    , logEntry.Application, logEntry.User, logEntry.Description, logEntry.ElementId));

            return HttpStatusCode.InternalServerError;
        }
    }
}
