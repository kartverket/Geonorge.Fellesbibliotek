using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.LogEntry
{
    public interface ILogEntryService
    {
        Task<HttpStatusCode> AddLogEntry(LogEntry logEntry);
        Task<List<LogEntry>> GetEntriesForElement(string elementId, int limitNumberOfEntries = 10);
    }
}
