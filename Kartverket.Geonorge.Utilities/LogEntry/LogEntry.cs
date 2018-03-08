using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartverket.Geonorge.Utilities.LogEntry
{
    public class LogEntry
    {
        /// <summary>
        ///     Unique identifier for this entry
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Date and time for when the event happened.
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        ///     The application where the event happened.
        ///     /// Valid values: Metadataeditor, Register, Kartkatalog, Kartografiregister, Symbolregister, Produktark
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        ///     The username of the user that made the change.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Description of what happened. E.g Adjusted extent in metadata.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The identificator of the element that has changed, e.g. the metadata uuid
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        ///     Type of operation: Added, Modified, Deleted
        /// </summary>
        public string Operation { get; set; }
    }

    public static class Operation
    {        
        public const string Added = "Added";
        public const string Modified = "Modified";
        public const string Deleted = "Deleted";
    }
}
