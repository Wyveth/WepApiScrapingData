using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.Utils;

namespace WebApiScrapingData.Test.Utils
{
    [TestFixture]
    public class TranslationUtilsTest
    {
        [Test]
        [TestCase("Niveau 16")]
        public void Translate(string value)
        {
            PokemonJson dataJson = new();
            dataJson.FR.WhenEvolution = value;
            
            TranslationUtils.Translate(dataJson, Constantes.Level_FR);

            Assert.NotNull(dataJson.EN.WhenEvolution);
            Assert.That(dataJson.EN.WhenEvolution, Is.EqualTo("Level 16"));
            Assert.NotNull(dataJson.ES.WhenEvolution);
            Assert.That(dataJson.ES.WhenEvolution, Is.EqualTo("Nivel 16"));
            Assert.NotNull(dataJson.IT.WhenEvolution);
            Assert.That(dataJson.IT.WhenEvolution, Is.EqualTo("Livello 16"));
            Assert.NotNull(dataJson.DE.WhenEvolution);
            Assert.That(dataJson.DE.WhenEvolution, Is.EqualTo("Eben 16"));
            Assert.NotNull(dataJson.RU.WhenEvolution);
            Assert.That(dataJson.RU.WhenEvolution, Is.EqualTo("16 Yровень"));
            Assert.NotNull(dataJson.CO.WhenEvolution);
            Assert.That(dataJson.CO.WhenEvolution, Is.EqualTo("수준 16"));
            Assert.NotNull(dataJson.CN.WhenEvolution);
            Assert.That(dataJson.CN.WhenEvolution, Is.EqualTo("16 等级"));
            Assert.NotNull(dataJson.CN.WhenEvolution);
            Assert.That(dataJson.JP.WhenEvolution, Is.EqualTo("レベル 16"));
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void getTranslateWithUrl(bool includeRussian)
        {
            const string html = "<html><body><h1 lang=\"en\">Venusaurite</h1>" +
                "<span lang=\"ja\">フシギバナイト</span><table>Language" +
                "<tr>\n<td>Spain</td>\n<td></td>\n<td>Venusaurita</td>\n</tr>" +
                "<tr>\n<td>Italian</td>\n<td></td>\n<td>Venusaurite</td>\n</tr>" +
                "<tr>\n<td>German</td>\n<td></td>\n<td>Bisaflornit</td>\n</tr>" +
                "<tr>\n<td>Russian</td>\n<td></td>\n<td>Венузаврит</td>\n</tr>" +
                "<tr>\n<td>Korean</td>\n<td></td>\n<td>이상해꽃나이</td>\n</tr>" +
                "<tr>\n<td>Mandarin</td>\n<td></td>\n<td>妙蛙花進化石</td>\n</tr>" +
                "</table></body></html>";
            using var server = new LocalHttpServer(html);
            Dictionary<string, string> dic = TranslationUtils.getTranslateWithUrl(server.Url, new(), includeRussian);

            Assert.NotNull(dic);
            Assert.That(dic[Constantes.EN], Is.EqualTo("Venusaurite"));
            Assert.NotNull(dic[Constantes.EN]);
            Assert.That(dic[Constantes.ES], Is.EqualTo("Venusaurita"));
            Assert.NotNull(dic[Constantes.ES]);
            Assert.That(dic[Constantes.IT], Is.EqualTo("Venusaurite"));
            Assert.NotNull(dic[Constantes.IT]);
            Assert.That(dic[Constantes.DE], Is.EqualTo("Bisaflornit"));
            Assert.NotNull(dic[Constantes.DE]); 
            if (includeRussian)
                Assert.That(dic[Constantes.RU], Is.EqualTo("Венузаврит"));
            else
                Assert.That(dic.ContainsKey(Constantes.RU), Is.False);
            Assert.NotNull(dic[Constantes.CO]);
            Assert.That(dic[Constantes.CO], Is.EqualTo("이상해꽃나이"));
            Assert.NotNull(dic[Constantes.CN]);
            Assert.That(dic[Constantes.CN], Is.EqualTo("妙蛙花進化石"));
            Assert.NotNull(dic[Constantes.JP]);
            Assert.That(dic[Constantes.JP], Is.EqualTo("フシギバナイト"));
        }
    }
}
