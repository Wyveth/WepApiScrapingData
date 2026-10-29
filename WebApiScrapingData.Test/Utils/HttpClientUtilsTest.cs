using HtmlAgilityPack;
using WepApiScrapingData.Utils;

namespace WebApiScrapingData.Test.Utils
{
    [TestFixture]
    public class HttpClientUtilsTest
    {
        [Test]
        public void CallUrlRejectsHttpError()
        {
            using var server = new LocalHttpServer("<html>Forbidden</html>", 403);
            var exception = Assert.ThrowsAsync<HttpRequestException>(() => HttpClientUtils.CallUrl(server.Url));
            Assert.That(exception!.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
        }

        [Test]
        public async Task CallUrl()
        {
            using var server = new LocalHttpServer("<html><body>Bulbizarre</body></html>");
            var result = await HttpClientUtils.CallUrl(server.Url);
            Assert.That(result, Is.EqualTo("<html><body>Bulbizarre</body></html>"));
        }
        
        [Test]
        public void CallUrlDynamic()
        {
            using var server = new LocalHttpServer("<html><body><h1>Venusaurite</h1></body></html>");
            HtmlNode result = HttpClientUtils.CallUrlDynamic(server.Url);
            Assert.That(result.SelectSingleNode("//h1").InnerText, Is.EqualTo("Venusaurite"));
        }
    }
}
