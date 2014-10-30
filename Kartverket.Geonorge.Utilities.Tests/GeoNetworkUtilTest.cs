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

    }
}
