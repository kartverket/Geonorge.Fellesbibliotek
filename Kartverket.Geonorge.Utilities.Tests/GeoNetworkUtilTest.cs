using FluentAssertions;
using NUnit.Framework;

namespace Kartverket.Geonorge.Utilities.Tests
{
    class GeoNetworkUtilTest
    {
        private const string BaseUrl = "http://www.geonorge.no/geonetwork/";
        private const string Uuid = "4403b8b6-6c10-4570-b402-8633e03a606e";

        [Test]
        public void ShouldReturnViewUrl()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string viewUrl = util.GetViewUrl(Uuid);

            viewUrl.Should().Be(BaseUrl + "?uuid=" + Uuid);
        }

        [Test]
        public void ShouldReturnXmlDownloadUrl()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string xmlDownloadUrl = util.GetXmlDownloadUrl(Uuid);

            xmlDownloadUrl.Should().Be(BaseUrl + "srv/nor/xml_iso19139?uuid=" + Uuid);
        }

        [Test]
        public void ShouldReturnOriginalThumbnailUrlWhenNull()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string thumbnailUrl = util.GetThumbnailUrl(Uuid, null);

            thumbnailUrl.Should().BeNull();
        }

        [Test]
        public void ShouldReturnOriginalThumbnailUrlWhenEmpty()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string thumbnailUrl = util.GetThumbnailUrl(Uuid, "");

            thumbnailUrl.Should().Be("");
        }

        [Test]
        public void ShouldReturnOriginalThumbnailUrlWhenUrlStartsWithHttp()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string exampleImage = "http://example.com/myimage.png";
            string thumbnailUrl = util.GetThumbnailUrl(Uuid, exampleImage);

            thumbnailUrl.Should().Be(exampleImage);
        }

        [Test]
        public void ShouldReturnThumbnailUrlFromGeoNetworkWhenThumbnailOnlyContainsFilename()
        {
            var util = new GeoNetworkUtil(BaseUrl);
            string exampleImage = "myimage.png";
            string thumbnailUrl = util.GetThumbnailUrl(Uuid, exampleImage);

            thumbnailUrl.Should().Be(BaseUrl + "srv/nor/resources.get?uuid=" + Uuid + "&access=public&fname=" + exampleImage);
        }
    }
}
