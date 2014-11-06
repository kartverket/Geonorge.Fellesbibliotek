namespace Kartverket.Geonorge.Utilities
{
    public class GeoNetworkUtil
    {
        private readonly string _baseUrl;

        public GeoNetworkUtil(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string GetViewUrl(string uuid)
        {
            return string.Format("{0}?uuid={1}", _baseUrl, uuid);
        }

        public string GetXmlDownloadUrl(string uuid)
        {
            return string.Format("{0}srv/nor/xml_iso19139?uuid={1}", _baseUrl, uuid);
        }

        public string GetThumbnailUrl(string uuid, string thumbnail)
        {
            if (!string.IsNullOrWhiteSpace(thumbnail) && !thumbnail.StartsWith("http"))
            {
                 return string.Format("{0}srv/nor/resources.get?uuid={1}&access=public&fname={2}", _baseUrl, uuid, thumbnail);
            }
            return thumbnail;
        }
    }
}
