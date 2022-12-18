using HtmlAgilityPack;
using WepApiScrapingData.Utils;

namespace WebApiScrapingData.Test.Utils
{
    [TestFixture]
    public class HttpClientUtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(Constantes.urlStart1Gen_FR)]
        public async Task CallUrl(string fullUrl)
        {
            var result = await HttpClientUtils.CallUrl(Constantes.urlStart1Gen_FR);
            Assert.NotNull(result);
            Assert.IsInstanceOf<string>(result);
        }
        
        [Test]
        [TestCase(Constantes.Venusaurite_URL)]
        public void CallUrlDynamic(string fullUrl)
        {
            var result = HttpClientUtils.CallUrlDynamic(fullUrl);
            Assert.NotNull(result);
            Assert.IsInstanceOf<HtmlNode>(result);
        }
    }
}