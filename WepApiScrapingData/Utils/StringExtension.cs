using System.Text;
using WebApiScrapingData.Domain;

namespace WepApiScrapingData.Utils
{
    public static class StringExtension
    {
        public static string translationClean(this string s, string value1, string translateValue1, string value2 = "", string translateValue2 = "")
        {
            if (!string.IsNullOrEmpty(value2))
                return new StringBuilder(s)
                  .Replace(value1, translateValue1)
                  .Replace(value2, translateValue2)
                  .ToString();
            else
                return new StringBuilder(s)
                  .Replace(value1, translateValue1)
                  .ToString();
        }

        public static string translationCleanRU(this string s, string value1, string translateValue1, string value2 = "", string translateValue2 = "")
        {
            if (value1.Equals(Constantes.Level_FR))
            {
                translateValue1 = s.Split(' ')[1] + " " + s.Split(' ')[0].Replace(Constantes.Level_FR, Constantes.Level_RU);
                return new StringBuilder(s)
                      .Replace(value1 + " " + s.Split(' ')[1], translateValue1)
                      .Replace(value2, translateValue2)
                      .ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(value2))
                    return new StringBuilder(s)
                      .Replace(value1, translateValue1)
                      .Replace(value2, translateValue2)
                      .ToString();
                else
                    return new StringBuilder(s)
                      .Replace(value1, translateValue1)
                      .ToString();
            }
        }
    }
}
