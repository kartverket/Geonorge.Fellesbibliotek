namespace Kartverket.Geonorge.Utilities
{
    public class GeonorgeUrlResolver : IGeonorgeUrlResolver
    {
        private readonly string _editorUrl;

        public GeonorgeUrlResolver(string editorUrl)
        {
            _editorUrl = editorUrl;
        }

        public string EditMetadata(string uuid)
        {
            return string.Format("{0}Metadata/Edit?uuid={1}", _editorUrl, uuid);
        }
    }
}
