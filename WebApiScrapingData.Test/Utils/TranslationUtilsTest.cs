using NUnit.Framework.Legacy;
using WebApiScrapingData.Domain.ClassJson;
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

            ClassicAssert.NotNull(dataJson.EN.WhenEvolution);
            Assert.That(dataJson.EN.WhenEvolution, Is.EqualTo("Level 16"));
            ClassicAssert.NotNull(dataJson.ES.WhenEvolution);
            Assert.That(dataJson.ES.WhenEvolution, Is.EqualTo("Nivel 16"));
            ClassicAssert.NotNull(dataJson.IT.WhenEvolution);
            Assert.That(dataJson.IT.WhenEvolution, Is.EqualTo("Livello 16"));
            ClassicAssert.NotNull(dataJson.DE.WhenEvolution);
            Assert.That(dataJson.DE.WhenEvolution, Is.EqualTo("Eben 16"));
            ClassicAssert.NotNull(dataJson.RU.WhenEvolution);
            Assert.That(dataJson.RU.WhenEvolution, Is.EqualTo("16 Yровень"));
            ClassicAssert.NotNull(dataJson.CO.WhenEvolution);
            Assert.That(dataJson.CO.WhenEvolution, Is.EqualTo("수준 16"));
            ClassicAssert.NotNull(dataJson.CN.WhenEvolution);
            Assert.That(dataJson.CN.WhenEvolution, Is.EqualTo("16 等级"));
            ClassicAssert.NotNull(dataJson.CN.WhenEvolution);
            Assert.That(dataJson.JP.WhenEvolution, Is.EqualTo("レベル 16"));
        }

        [Test]
        [TestCase(Constantes.Venusaurite_URL, Constantes.Venusaurite_RU, false)]
        public void getTranslateWithUrl(string url, string RU_Value, bool RU)
        {
            Dictionary<string, string> dic = TranslationUtils.getTranslateWithUrl(url, new(), RU);

            ClassicAssert.NotNull(dic);
            Assert.That(dic[Constantes.EN], Is.EqualTo("Venusaurite"));
            ClassicAssert.NotNull(dic[Constantes.EN]);
            Assert.That(dic[Constantes.ES], Is.EqualTo("Venusaurita"));
            ClassicAssert.NotNull(dic[Constantes.ES]);
            Assert.That(dic[Constantes.IT], Is.EqualTo("Venusaurite"));
            ClassicAssert.NotNull(dic[Constantes.IT]);
            Assert.That(dic[Constantes.DE], Is.EqualTo("Bisaflornit"));
            ClassicAssert.NotNull(dic[Constantes.DE]); 
            Assert.That(RU_Value, Is.EqualTo("Венузаврит"));
            ClassicAssert.NotNull(dic[Constantes.CO]);
            Assert.That(dic[Constantes.CO], Is.EqualTo("이상해꽃나이"));
            ClassicAssert.NotNull(dic[Constantes.CN]);
            Assert.That(dic[Constantes.CN], Is.EqualTo("妙蛙花進化石"));
            ClassicAssert.NotNull(dic[Constantes.JP]);
            Assert.That(dic[Constantes.JP], Is.EqualTo("フシギバナイト"));
        }
    }
}
