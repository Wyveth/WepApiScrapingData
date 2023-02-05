using System.Text;

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

        public static string translationCleanOther(this string s, string value1, string translateValue1, string value2 = "", string translateValue2 = "")
        {
            if (value1.Equals(Constantes.Level_FR))
            {
                string stringFormatLevel = "";
                if (translateValue1.Equals(Constantes.Level_CO) || translateValue1.Equals(Constantes.Level_JP))
                {
                    if (translateValue1.Equals(Constantes.Level_CO))
                        stringFormatLevel = Constantes.Level_CO + " " + s.Split(',')[0].Split(' ')[1];
                    else
                        stringFormatLevel = Constantes.Level_JP + " " + s.Split(',')[0].Split(' ')[1];
                }
                else
                {
                    if (translateValue1.Equals(Constantes.Level_RU))                        
                        stringFormatLevel = s.Split(',')[0].Split(' ')[1] + " " + Constantes.Level_RU;
                    else
                        stringFormatLevel = s.Split(',')[0].Split(' ')[1] + " " + Constantes.Level_CN;
                }
                
                if (!string.IsNullOrEmpty(value2))
                {
                    return new StringBuilder(s)
                      .Replace(Constantes.Level_FR + " " + s.Split(',')[0].Split(' ')[1], stringFormatLevel)
                      .Replace(value2, translateValue2)
                      .ToString();
                }
                else {
                    return stringFormatLevel;
                }
            }
            else if (value2.Equals(Constantes.MegaEvolutionWith_FR) || value2.Equals(Constantes.GigantamaxForm_FR)) {
                return translateValue2 + " " + translateValue1;
            }
            else if (value2.Equals(Constantes.SwapWH_FR) || value2.Equals(Constantes.ExchangeA_FR))
            {
                return translateValue2 + ": " + translateValue1;
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
