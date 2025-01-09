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
            return string.Format("{0}srv/nor/csw?service=CSW&request=GetRecordById&version=2.0.2&outputSchema=http://www.isotc211.org/2005/gmd&elementSetName=full&id={1}", _baseUrl, uuid);
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
