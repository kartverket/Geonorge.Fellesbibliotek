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
    }
}
