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
        private readonly string _apiKey;
        private readonly IHttpClientFactory _factory;

        public LogEntryService(string logUrl, string apiKey, IHttpClientFactory factory)
        {
            _logUrl = logUrl;
            _factory = factory;
            _apiKey = apiKey;
        }
        public async Task<HttpStatusCode> AddLogEntry(LogEntry logEntry)
        {
            Logger.Debug(string.Format("Adding log entry: {0}", logEntry));

            HttpClient client = _factory.GetHttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Remove("apikey");
            client.DefaultRequestHeaders.Add("apikey", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(_logUrl + "api/logentry/add", logEntry).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsAsync<LogEntry>().ConfigureAwait(false);

                Logger.Debug(string.Format("LogEntry added. [Application={0}], [User={1}], [Description={2}], [ElementId={3}] , [Title={4}], [Operation={5}]"
                    , logEntry.Application, logEntry.User, logEntry.Description, logEntry.ElementId, logEntry.Title, logEntry.Operation));

                return HttpStatusCode.NoContent;
            }

            Logger.Error(string.Format("LogEntry not added. [Application={0}], [User={1}], [Description={2}], [ElementId={3}], [Title={4}], [Operation={5}]"
                    , logEntry.Application, logEntry.User, logEntry.Description, logEntry.ElementId, logEntry.Title, logEntry.Operation));

            return HttpStatusCode.InternalServerError;
        }

        public async Task<List<LogEntry>> GetEntriesForElement(string elementId, int limitNumberOfEntries = 10)
        {
            Logger.Debug(string.Format("Looking up LogEntry by elementId: {0}", elementId));

            HttpClient client = _factory.GetHttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Remove("apikey");
            client.DefaultRequestHeaders.Add("apikey", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(_logUrl + "api/logentry/list?elementId=" + elementId + "&limitNumberOfEntries=" + limitNumberOfEntries).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                List<LogEntry> logEntries = await response.Content.ReadAsAsync<List<LogEntry>>().ConfigureAwait(false);

                if (logEntries == null)
                    return null;

                Logger.Debug(string.Format("Query for elementId = [{0}] returned {1} elements", elementId, logEntries.Count));

                return logEntries;
            }

            Logger.Debug(string.Format("Log entry for elementId [{0}] not found. Http response code: {1}", elementId, response.StatusCode));

            return null;
        }
    }
}
