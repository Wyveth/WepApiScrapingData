using HtmlAgilityPack;
using PuppeteerSharp;
using WebApiScrapingData.Domain.ClassJson;

namespace WepApiScrapingData.Utils
{
    public static class TranslationUtils
    {
        public static void Translate(PokemonJson dataJson, string value)
        {
            switch (value)
            {
                case Constantes.Level_FR:
                    #region Level
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.PID_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.PID_FR, Constantes.PID_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.PID_FR, Constantes.PID_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.PID_FR, Constantes.PID_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.PID_FR, Constantes.PID_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.PID_FR, Constantes.PID_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.PID_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.PID_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.PID_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.ASD_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.ASD_FR, Constantes.ASD_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.ASD_FR, Constantes.ASD_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.ASD_FR, Constantes.ASD_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.ASD_FR, Constantes.ASD_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.ASD_FR, Constantes.ASD_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.ASD_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.ASD_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.ASD_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.AID_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AID_FR, Constantes.AID_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AID_FR, Constantes.AID_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AID_FR, Constantes.AID_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AID_FR, Constantes.AID_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AID_FR, Constantes.AID_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AID_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AID_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AID_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.AED_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AED_FR, Constantes.AED_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AED_FR, Constantes.AED_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AED_FR, Constantes.AED_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AED_FR, Constantes.AED_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AED_FR, Constantes.AED_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AED_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AED_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AED_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.PIB_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.PIB_FR, Constantes.PIB_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.PIB_FR, Constantes.PIB_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.PIB_FR, Constantes.PIB_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.PIB_FR, Constantes.PIB_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.PIB_FR, Constantes.PIB_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.PIB_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.PIB_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.PIB_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.FM_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.FM_FR, Constantes.FM_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.FM_FR, Constantes.FM_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.FM_FR, Constantes.FM_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.FM_FR, Constantes.FM_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.FM_FR, Constantes.FM_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.FM_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.FM_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.FM_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.M_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.M_FR, Constantes.M_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.M_FR, Constantes.M_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.M_FR, Constantes.M_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.M_FR, Constantes.M_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.M_FR, Constantes.M_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.M_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.M_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.M_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.D_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.D_FR, Constantes.D_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.D_FR, Constantes.D_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.D_FR, Constantes.D_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.D_FR, Constantes.D_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.D_FR, Constantes.D_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.D_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.D_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.D_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.N_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.N_FR, Constantes.N_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.N_FR, Constantes.N_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.N_FR, Constantes.N_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.N_FR, Constantes.N_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.N_FR, Constantes.N_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.N_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.N_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.N_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.DLUL_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.DLUL_FR, Constantes.DLUL_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.DLUL_FR, Constantes.DLUL_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.DLUL_FR, Constantes.DLUL_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.DLUL_FR, Constantes.DLUL_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.DLUL_FR, Constantes.DLUL_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.DLUL_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.DLUL_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.DLUL_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.NSUS_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.NSUS_FR, Constantes.NSUS_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.NSUS_FR, Constantes.NSUS_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.NSUS_FR, Constantes.NSUS_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.NSUS_FR, Constantes.NSUS_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.NSUS_FR, Constantes.NSUS_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.NSUS_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.NSUS_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.NSUS_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.CR_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.CR_FR, Constantes.CR_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.CR_FR, Constantes.CR_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.CR_FR, Constantes.CR_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.CR_FR, Constantes.CR_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.CR_FR, Constantes.CR_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.CR_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.CR_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.CR_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.OSA_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.OSA_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.OSA_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.OSA_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.CB_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.CB_FR, Constantes.CB_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.CB_FR, Constantes.CB_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.CB_FR, Constantes.CB_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.CB_FR, Constantes.CB_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.CB_FR, Constantes.CB_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.CB_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.CB_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.CB_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.SPN_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.SPN_FR, Constantes.SPN_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.SPN_FR, Constantes.SPN_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.SPN_FR, Constantes.SPN_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.SPN_FR, Constantes.SPN_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.SPN_FR, Constantes.SPN_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.SPN_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.SPN_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.SPN_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.WN_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.WN_FR, Constantes.WN_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.WN_FR, Constantes.WN_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.WN_FR, Constantes.WN_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.WN_FR, Constantes.WN_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.WN_FR, Constantes.WN_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.WN_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.WN_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.WN_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.DPIT_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.DPIT_FR, Constantes.DPIT_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.DPIT_FR, Constantes.DPIT_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.DPIT_FR, Constantes.DPIT_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.DPIT_FR, Constantes.DPIT_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.DPIT_FR, Constantes.DPIT_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.DPIT_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.DPIT_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.DPIT_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.OSG_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.OSG_FR, Constantes.OSG_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.OSG_FR, Constantes.OSG_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.OSG_FR, Constantes.OSG_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.OSG_FR, Constantes.OSG_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.OSG_FR, Constantes.OSG_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.OSG_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.OSG_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.OSG_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.AG_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.AG_FR, Constantes.AG_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.AG_FR, Constantes.AG_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.AG_FR, Constantes.AG_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.AG_FR, Constantes.AG_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.AG_FR, Constantes.AG_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.AG_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.AG_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.AG_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.NR_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.NR_FR, Constantes.NR_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.NR_FR, Constantes.NR_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.NR_FR, Constantes.NR_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.NR_FR, Constantes.NR_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.NR_FR, Constantes.NR_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.NR_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.NR_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.NR_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.SUSE_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.SUSE_FR, Constantes.SUSE_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.SUSE_FR, Constantes.SUSE_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.SUSE_FR, Constantes.SUSE_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.SUSE_FR, Constantes.SUSE_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.SUSE_FR, Constantes.SUSE_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.SUSE_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.SUSE_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.SUSE_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.LULB_FR))
                    {
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_EN, Constantes.LULB_FR, Constantes.LULB_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_ES, Constantes.LULB_FR, Constantes.LULB_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_IT, Constantes.LULB_FR, Constantes.LULB_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Level_FR, Constantes.Level_DE, Constantes.LULB_FR, Constantes.LULB_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_RU, Constantes.LULB_FR, Constantes.LULB_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CO, Constantes.PID_FR, Constantes.LULB_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_CN, Constantes.PID_FR, Constantes.LULB_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Level_FR, Constantes.Level_JP, Constantes.PID_FR, Constantes.LULB_JP);
                    }
                    else
                    {
                        string lvl = dataJson.FR.WhenEvolution.Split(" ")[1];
                        dataJson.EN.WhenEvolution = String.Format(Constantes.Level_Format_EN, lvl);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Level_Format_ES, lvl);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Level_Format_IT, lvl);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Level_Format_DE, lvl);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Level_Format_RU, lvl);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Level_Format_CO, lvl);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Level_Format_CN, lvl);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Level_Format_JP, lvl);
                    }
                    #endregion
                    break;
                case Constantes.Stone_FR:
                    #region Stone
                    Dictionary<string, string> dicTranslationStone = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.MoonStone_FR))
                    {
                        getTranslateWithUrl(Constantes.MoonStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.MoonStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.MoonStone_FR, Constantes.MoonStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.MoonStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.FireStone_FR))
                    {
                        getTranslateWithUrl(Constantes.FireStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.FireStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.FireStone_FR, Constantes.FireStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.FireStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.LeafStone_FR))
                    {
                        getTranslateWithUrl(Constantes.LeafStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.EN], Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.ES], Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.IT], Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.LeafStone_FR, dicTranslationStone[Constantes.DE], Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.LeafStone_FR, Constantes.LeafStone_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.LeafStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.WaterStone_FR))
                    {
                        getTranslateWithUrl(Constantes.WaterStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.WaterStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.WaterStone_FR, Constantes.WaterStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.WaterStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.ThunderStone_FR))
                    {
                        getTranslateWithUrl(Constantes.ThunderStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.EN], Constantes.OSA_FR, Constantes.OSA_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.ES], Constantes.OSA_FR, Constantes.OSA_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.IT], Constantes.OSA_FR, Constantes.OSA_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.DE], Constantes.OSA_FR, Constantes.OSA_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ThunderStone_FR, Constantes.ThunderStone_RU, Constantes.OSA_FR, Constantes.OSA_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.CO], Constantes.OSA_FR, Constantes.OSA_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.CN], Constantes.OSA_FR, Constantes.OSA_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ThunderStone_FR, dicTranslationStone[Constantes.JP], Constantes.OSA_FR, Constantes.OSA_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.SunStone_FR))
                    {
                        getTranslateWithUrl(Constantes.SunStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.SunStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.SunStone_FR, Constantes.SunStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.SunStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.ShinyStone_FR))
                    {
                        getTranslateWithUrl(Constantes.ShinyStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ShinyStone_FR, Constantes.ShinyStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.ShinyStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.DuckStone_FR))
                    {
                        getTranslateWithUrl(Constantes.DuckStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DuckStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DuckStone_FR, Constantes.DuckStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DuckStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.IceStone_FR))
                    {
                        getTranslateWithUrl(Constantes.IceStone_URL, dicTranslationStone);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.EN]);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.ES]);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.IT]);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.IceStone_FR, dicTranslationStone[Constantes.DE]);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.IceStone_FR, Constantes.IceStone_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.CO]);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.CN]);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.IceStone_FR, dicTranslationStone[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.DawnStone_FR))
                    {
                        getTranslateWithUrl(Constantes.DawnStone_URL, dicTranslationStone);

                        if (dataJson.FR.WhenEvolution.Contains(Constantes.M_FR))
                        {
                            dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN], Constantes.M_FR, Constantes.M_EN);
                            dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES], Constantes.M_FR, Constantes.M_ES);
                            dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT], Constantes.M_FR, Constantes.M_IT);
                            dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE], Constantes.M_FR, Constantes.M_DE);
                            dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU, Constantes.M_FR, Constantes.M_RU);
                            dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO], Constantes.M_FR, Constantes.M_CO);
                            dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN], Constantes.M_FR, Constantes.M_CN);
                            dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP], Constantes.M_FR, Constantes.M_JP);
                        }
                        else if (dataJson.FR.WhenEvolution.Contains(Constantes.FM_FR))
                        {
                            dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN], Constantes.FM_FR, Constantes.OSA_EN);
                            dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES], Constantes.FM_FR, Constantes.OSA_ES);
                            dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT], Constantes.FM_FR, Constantes.OSA_IT);
                            dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE], Constantes.FM_FR, Constantes.OSA_DE);
                            dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU, Constantes.FM_FR, Constantes.OSA_RU);
                            dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO], Constantes.FM_FR, Constantes.OSA_CO);
                            dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN], Constantes.FM_FR, Constantes.OSA_CN);
                            dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP], Constantes.FM_FR, Constantes.OSA_JP);
                        }
                        else
                        {
                            dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.EN]);
                            dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.ES]);
                            dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.IT]);
                            dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.DawnStone_FR, dicTranslationStone[Constantes.DE]);
                            dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, Constantes.DawnStone_RU);
                            dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CO]);
                            dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.CN]);
                            dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.DawnStone_FR, dicTranslationStone[Constantes.JP]);
                        }
                    }
                    #endregion
                    break;
                case Constantes.MegaEvolutionWith_FR:
                    #region Méga Evolution
                    Dictionary<string, string> dicTranslationMega = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.Venusaurite_FR))
                    {
                        getTranslateWithUrl(Constantes.Venusaurite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Venusaurite_FR, Constantes.Venusaurite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Venusaurite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Charizardite_X_FR))
                    {
                        getTranslateWithUrl(Constantes.Charizardite_X_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_X_FR, Constantes.Charizardite_X_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_X_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Charizardite_Y_FR))
                    {
                        getTranslateWithUrl(Constantes.Charizardite_Y_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Charizardite_Y_FR, Constantes.Charizardite_Y_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Charizardite_Y_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Blastoisinite_FR))
                    {
                        getTranslateWithUrl(Constantes.Blastoisinite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blastoisinite_FR, Constantes.Blastoisinite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blastoisinite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Beedrillite_FR))
                    {
                        getTranslateWithUrl(Constantes.Beedrillite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Beedrillite_FR, Constantes.Beedrillite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Beedrillite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Pidgeotite_FR))
                    {
                        getTranslateWithUrl(Constantes.Pidgeotite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pidgeotite_FR, Constantes.Pidgeotite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pidgeotite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Alakazite_FR))
                    {
                        getTranslateWithUrl(Constantes.Alakazite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Alakazite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Alakazite_FR, Constantes.Alakazite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Alakazite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Slowbronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Slowbronite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Slowbronite_FR, Constantes.Slowbronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Slowbronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Gengarite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gengarite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gengarite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gengarite_FR, Constantes.Gengarite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gengarite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Kangaskhanite_FR))
                    {
                        getTranslateWithUrl(Constantes.Kangaskhanite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kangaskhanite_FR, Constantes.Kangaskhanite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kangaskhanite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Pinsirite_FR))
                    {
                        getTranslateWithUrl(Constantes.Pinsirite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Pinsirite_FR, Constantes.Pinsirite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Pinsirite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Gyaradosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gyaradosite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gyaradosite_FR, Constantes.Gyaradosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gyaradosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Aerodactylite_FR))
                    {
                        getTranslateWithUrl(Constantes.Aerodactylite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aerodactylite_FR, Constantes.Aerodactylite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aerodactylite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Mewtwonite_X_FR))
                    {
                        getTranslateWithUrl(Constantes.Mewtwonite_X_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_X_FR, Constantes.Mewtwonite_X_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_X_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Mewtwonite_Y_FR))
                    {
                        getTranslateWithUrl(Constantes.Mewtwonite_Y_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mewtwonite_Y_FR, Constantes.Mewtwonite_Y_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mewtwonite_Y_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Ampharosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Ampharosite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Ampharosite_FR, Constantes.Ampharosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Ampharosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Steelixite_FR))
                    {
                        getTranslateWithUrl(Constantes.Steelixite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Steelixite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Steelixite_FR, Constantes.Steelixite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Steelixite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Scizorite_FR))
                    {
                        getTranslateWithUrl(Constantes.Scizorite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Scizorite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Scizorite_FR, Constantes.Scizorite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Scizorite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Heracronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Heracronite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Heracronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Heracronite_FR, Constantes.Heracronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Heracronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Houndoominite_FR))
                    {
                        getTranslateWithUrl(Constantes.Houndoominite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Houndoominite_FR, Constantes.Houndoominite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Houndoominite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Tyranitarite_FR))
                    {
                        getTranslateWithUrl(Constantes.Tyranitarite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Tyranitarite_FR, Constantes.Tyranitarite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Tyranitarite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sceptilite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sceptilite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sceptilite_FR, Constantes.Sceptilite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sceptilite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Blazikenite_FR))
                    {
                        getTranslateWithUrl(Constantes.Blazikenite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Blazikenite_FR, Constantes.Blazikenite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Blazikenite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Swampertite_FR))
                    {
                        getTranslateWithUrl(Constantes.Swampertite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Swampertite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Swampertite_FR, Constantes.Swampertite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Swampertite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Gardevoirite_FR))
                    {
                        getTranslateWithUrl(Constantes.Gardevoirite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Gardevoirite_FR, Constantes.Gardevoirite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Gardevoirite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sablenite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sablenite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sablenite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sablenite_FR, Constantes.Sablenite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sablenite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Mawilite_FR))
                    {
                        getTranslateWithUrl(Constantes.Mawilite_URL, dicTranslationMega);

                        dataJson.FR.WhenEvolution = Constantes.MegaEvolutionWith_FR + " " + Constantes.Mawilite_FR;
                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mawilite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Mawilite_FR, Constantes.Mawilite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Mawilite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Aggronite_FR))
                    {
                        getTranslateWithUrl(Constantes.Aggronite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aggronite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Aggronite_FR, Constantes.Aggronite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Aggronite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Medichamite_FR))
                    {
                        getTranslateWithUrl(Constantes.Medichamite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Medichamite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Medichamite_FR, Constantes.Medichamite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Medichamite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Manectite_FR))
                    {
                        getTranslateWithUrl(Constantes.Manectite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Manectite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Manectite_FR, Constantes.Manectite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Manectite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sharpedonite_FR))
                    {
                        getTranslateWithUrl(Constantes.Sharpedonite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Sharpedonite_FR, Constantes.Sharpedonite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Sharpedonite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Cameruptite_FR))
                    {
                        getTranslateWithUrl(Constantes.Cameruptite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Cameruptite_FR, Constantes.Cameruptite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Cameruptite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Altarianite_FR))
                    {
                        getTranslateWithUrl(Constantes.Altarianite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Altarianite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Altarianite_FR, Constantes.Altarianite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Altarianite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Banettite_FR))
                    {
                        getTranslateWithUrl(Constantes.Banettite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Banettite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Banettite_FR, Constantes.Banettite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Banettite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Absolite_FR))
                    {
                        getTranslateWithUrl(Constantes.Absolite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Absolite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Absolite_FR, Constantes.Absolite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Absolite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Glalitite_FR))
                    {
                        getTranslateWithUrl(Constantes.Glalitite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Glalitite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Glalitite_FR, Constantes.Glalitite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Glalitite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Salamencite_FR))
                    {
                        getTranslateWithUrl(Constantes.Salamencite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Salamencite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Salamencite_FR, Constantes.Salamencite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Salamencite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Metagrossite_FR))
                    {
                        getTranslateWithUrl(Constantes.Metagrossite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metagrossite_FR, Constantes.Metagrossite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metagrossite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Latiasite_FR))
                    {
                        getTranslateWithUrl(Constantes.Latiasite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiasite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiasite_FR, Constantes.Latiasite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiasite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Latiosite_FR))
                    {
                        getTranslateWithUrl(Constantes.Latiosite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiosite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Latiosite_FR, Constantes.Latiosite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Latiosite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Lopunnite_FR))
                    {
                        getTranslateWithUrl(Constantes.Lopunnite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lopunnite_FR, Constantes.Lopunnite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lopunnite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Garchompite_FR))
                    {
                        getTranslateWithUrl(Constantes.Garchompite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Garchompite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Garchompite_FR, Constantes.Garchompite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Garchompite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Lucarionite_FR))
                    {
                        getTranslateWithUrl(Constantes.Lucarionite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Lucarionite_FR, Constantes.Lucarionite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Lucarionite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Abomasite_FR))
                    {
                        getTranslateWithUrl(Constantes.Abomasite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Abomasite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Abomasite_FR, Constantes.Abomasite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Abomasite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Galladite_FR))
                    {
                        getTranslateWithUrl(Constantes.Galladite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Galladite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Galladite_FR, Constantes.Galladite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Galladite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Audinite_FR))
                    {
                        getTranslateWithUrl(Constantes.Audinite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Audinite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Audinite_FR, Constantes.Audinite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Audinite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Diancite_FR))
                    {
                        getTranslateWithUrl(Constantes.Diancite_URL, dicTranslationMega);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.EN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.ES], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.IT], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Diancite_FR, dicTranslationMega[Constantes.DE], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Diancite_FR, Constantes.Diancite_RU, Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.CO], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.CN], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Diancite_FR, dicTranslationMega[Constantes.JP], Constantes.MegaEvolutionWith_FR, Constantes.MegaEvolutionWith_JP);
                    }
                    #endregion
                    break;
                case Constantes.GigantamaxForm_FR:
                    #region Gigamax Form
                    dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_EN, dataJson.FR.DisplayName, dataJson.EN.DisplayName);
                    dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_ES, dataJson.FR.DisplayName, dataJson.ES.DisplayName);
                    dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_IT, dataJson.FR.DisplayName, dataJson.IT.DisplayName);
                    dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_DE, dataJson.FR.DisplayName, dataJson.DE.DisplayName);
                    dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_RU, dataJson.FR.DisplayName, dataJson.RU.DisplayName);
                    dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_CO, dataJson.FR.DisplayName, dataJson.CO.DisplayName);
                    dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_CN, dataJson.FR.DisplayName, dataJson.CN.DisplayName);
                    dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.GigantamaxFormOf_FR, Constantes.GigantamaxFormOf_JP, dataJson.FR.DisplayName, dataJson.JP.DisplayName);
                    #endregion
                    break;
                case Constantes.Exchange_FR:
                    #region Exchange
                    Dictionary<string, string> dicTranslationExchange = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.Kings_Rock_FR))
                    {
                        getTranslateWithUrl(Constantes.Kings_Rock_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kings_Rock_FR, Constantes.Kings_Rock_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Kings_Rock_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Metal_Coat_FR))
                    {
                        getTranslateWithUrl(Constantes.Metal_Coat_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metal_Coat_FR, Constantes.Metal_Coat_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Metal_Coat_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Dragon_Scale_FR))
                    {
                        getTranslateWithUrl(Constantes.Dragon_Scale_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dragon_Scale_FR, Constantes.Dragon_Scale_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dragon_Scale_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Upgrade_FR))
                    {
                        getTranslateWithUrl(Constantes.Upgrade_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Upgrade_FR, Constantes.Upgrade_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Upgrade_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Prism_Scale_FR))
                    {
                        getTranslateWithUrl(Constantes.Prism_Scale_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Prism_Scale_FR, Constantes.Prism_Scale_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Prism_Scale_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Deep_Sea_Tooth_FR))
                    {
                        getTranslateWithUrl(Constantes.Deep_Sea_Tooth_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Tooth_FR, Constantes.Deep_Sea_Tooth_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Tooth_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Deep_Sea_Scale_FR))
                    {
                        getTranslateWithUrl(Constantes.Deep_Sea_Scale_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Scale_FR, Constantes.Deep_Sea_Scale_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Deep_Sea_Scale_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Shelmet_FR))
                    {
                        getTranslateWithUrl(Constantes.Shelmet_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.EN], Constantes.ExchangeA_FR, Constantes.ExchangeA_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.ES], Constantes.ExchangeA_FR, Constantes.ExchangeA_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.IT], Constantes.ExchangeA_FR, Constantes.ExchangeA_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.DE], Constantes.ExchangeA_FR, Constantes.ExchangeA_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Shelmet_FR, Constantes.Shelmet_RU, Constantes.ExchangeA_FR, Constantes.ExchangeA_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.CO], Constantes.ExchangeA_FR, Constantes.ExchangeA_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.CN], Constantes.ExchangeA_FR, Constantes.ExchangeA_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Shelmet_FR, dicTranslationExchange[Constantes.JP], Constantes.ExchangeA_FR, Constantes.ExchangeA_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Karrablast_FR))
                    {
                        getTranslateWithUrl(Constantes.Karrablast_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.EN], Constantes.ExchangeA_FR, Constantes.ExchangeA_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.ES], Constantes.ExchangeA_FR, Constantes.ExchangeA_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.IT], Constantes.ExchangeA_FR, Constantes.ExchangeA_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.DE], Constantes.ExchangeA_FR, Constantes.ExchangeA_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, Constantes.Karrablast_RU, Constantes.ExchangeA_FR, Constantes.ExchangeA_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.CO], Constantes.ExchangeA_FR, Constantes.ExchangeA_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.CN], Constantes.ExchangeA_FR, Constantes.ExchangeA_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.JP], Constantes.ExchangeA_FR, Constantes.ExchangeA_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sachet_FR))
                    {
                        getTranslateWithUrl(Constantes.Karrablast_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, Constantes.Karrablast_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Karrablast_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Whipped_Dream_FR))
                    {
                        getTranslateWithUrl(Constantes.Whipped_Dream_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Whipped_Dream_FR, Constantes.Whipped_Dream_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Whipped_Dream_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Protector_FR))
                    {
                        getTranslateWithUrl(Constantes.Protector_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Protector_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Protector_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Protector_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Protector_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Protector_FR, Constantes.Protector_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Protector_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Protector_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Protector_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Electirizer_FR))
                    {
                        getTranslateWithUrl(Constantes.Electirizer_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Electirizer_FR, Constantes.Electirizer_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Electirizer_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Magmarizer_FR))
                    {
                        getTranslateWithUrl(Constantes.Magmarizer_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Magmarizer_FR, Constantes.Magmarizer_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Magmarizer_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Dubious_Disc_FR))
                    {
                        getTranslateWithUrl(Constantes.Dubious_Disc_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dubious_Disc_FR, Constantes.Dubious_Disc_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Dubious_Disc_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Reaper_Cloth_FR))
                    {
                        getTranslateWithUrl(Constantes.Reaper_Cloth_URL, dicTranslationExchange);

                        dataJson.EN.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.EN], Constantes.SwapWH_FR, Constantes.SwapWH_EN);
                        dataJson.ES.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.ES], Constantes.SwapWH_FR, Constantes.SwapWH_ES);
                        dataJson.IT.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.IT], Constantes.SwapWH_FR, Constantes.SwapWH_IT);
                        dataJson.DE.WhenEvolution = dataJson.FR.WhenEvolution.translationClean(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.DE], Constantes.SwapWH_FR, Constantes.SwapWH_DE);
                        dataJson.RU.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Reaper_Cloth_FR, Constantes.Reaper_Cloth_RU, Constantes.SwapWH_FR, Constantes.SwapWH_RU);
                        dataJson.CO.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.CO], Constantes.SwapWH_FR, Constantes.SwapWH_CO);
                        dataJson.CN.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.CN], Constantes.SwapWH_FR, Constantes.SwapWH_CN);
                        dataJson.JP.WhenEvolution = dataJson.FR.WhenEvolution.translationCleanOther(Constantes.Reaper_Cloth_FR, dicTranslationExchange[Constantes.JP], Constantes.SwapWH_FR, Constantes.SwapWH_JP);
                    }
                    #endregion
                    break;
                case Constantes.Reproduction_FR:
                    #region Reproduction
                    Dictionary<string, string> dicTranslationReproduction = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.Jynx_FR))
                    {
                        getTranslateWithUrl(Constantes.Jynx_URL, dicTranslationReproduction, true);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_RU, dicTranslationReproduction[Constantes.RU]);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Lucario_FR))
                    {
                        getTranslateWithUrl(Constantes.Lucario_URL, dicTranslationReproduction, true);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_RU, dicTranslationReproduction[Constantes.RU]);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Toxtricity_FR))
                    {
                        getTranslateWithUrl(Constantes.Toxtricity_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_RU, Constantes.Toxtricity_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_1_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.PikachuRaichu_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Pikachu_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Raichu_URL, new(), true));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_RU, listDic[0].ContainsKey(Constantes.RU), listDic[1].ContainsKey(Constantes.RU));
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.JigglypuffWigglytuff_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Jigglypuff_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Wigglytuff_URL, new(), true));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_RU, listDic[0].ContainsKey(Constantes.RU), listDic[1].ContainsKey(Constantes.RU));
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.TogepiTogetic_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Togepi_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Togetic_URL, new(), true));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_RU, listDic[0].ContainsKey(Constantes.RU), listDic[1].ContainsKey(Constantes.RU));
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.ElectabuzzElectivire_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Electabuzz_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Electivire_URL, new()));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_RU, listDic[0].ContainsKey(Constantes.RU), Constantes.Electivire_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.MagmarMagmortar_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Magmar_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Magmortar_URL, new()));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_RU, listDic[0].ContainsKey(Constantes.RU), Constantes.Magmortar_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_2_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.HitmonchanHitmonleeHitmontop_FR))
                    {
                        List<Dictionary<string, string>> listDic = new();
                        listDic.Add(getTranslateWithUrl(Constantes.Hitmonchan_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Hitmonlee_URL, new(), true));
                        listDic.Add(getTranslateWithUrl(Constantes.Hitmontop_URL, new(), true));

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_EN, listDic[0].ContainsKey(Constantes.EN), listDic[1].ContainsKey(Constantes.EN), listDic[2].ContainsKey(Constantes.EN));
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_ES, listDic[0].ContainsKey(Constantes.ES), listDic[1].ContainsKey(Constantes.ES), listDic[2].ContainsKey(Constantes.ES));
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_IT, listDic[0].ContainsKey(Constantes.IT), listDic[1].ContainsKey(Constantes.IT), listDic[2].ContainsKey(Constantes.IT));
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_DE, listDic[0].ContainsKey(Constantes.DE), listDic[1].ContainsKey(Constantes.DE), listDic[2].ContainsKey(Constantes.DE));
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_RU, listDic[0].ContainsKey(Constantes.RU), listDic[1].ContainsKey(Constantes.RU), listDic[2].ContainsKey(Constantes.RU));
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_CO, listDic[0].ContainsKey(Constantes.CO), listDic[1].ContainsKey(Constantes.CO), listDic[2].ContainsKey(Constantes.CO));
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_CN, listDic[0].ContainsKey(Constantes.CN), listDic[1].ContainsKey(Constantes.CN), listDic[2].ContainsKey(Constantes.CN));
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Reproduction_Format_3_JP, listDic[0].ContainsKey(Constantes.JP), listDic[1].ContainsKey(Constantes.JP), listDic[2].ContainsKey(Constantes.JP));
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sea_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Sea_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Sea_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Lax_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Lax_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Lax_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Pure_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Pure_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Pure_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Rock_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Rock_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Rock_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Odd_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Odd_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Odd_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Luck_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Luck_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Luck_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Full_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Full_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Full_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Wave_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Wave_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Wave_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Rose_Incense_FR))
                    {
                        getTranslateWithUrl(Constantes.Rose_Incense_URL, dicTranslationReproduction);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_EN, dicTranslationReproduction[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_ES, dicTranslationReproduction[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_IT, dicTranslationReproduction[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_DE, dicTranslationReproduction[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_RU, Constantes.Rose_Incense_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CO, dicTranslationReproduction[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_CN, dicTranslationReproduction[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.ReproductionWithHeldByParent_Format_JP, dicTranslationReproduction[Constantes.JP]);
                    }
                    #endregion
                    break;
                case Constantes.LvlUpWith_FR:
                    #region LvlUpWith
                    Dictionary<string, string> dicTranslationLvlUp = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.HighHappiness_FR))
                    {
                        if (dataJson.FR.WhenEvolution.Contains(Constantes.D_FR))
                        {
                            dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_EN, Constantes.HighHappiness_EN, Constantes.D_EN);
                            dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_ES, Constantes.HighHappiness_ES, Constantes.D_ES);
                            dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_IT, Constantes.HighHappiness_IT, Constantes.D_IT);
                            dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_DE, Constantes.HighHappiness_DE, Constantes.D_DE);
                            dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_RU, Constantes.HighHappiness_RU, Constantes.D_RU);
                            dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_CO, Constantes.HighHappiness_CO, Constantes.D_CO);
                            dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_CN, Constantes.HighHappiness_CN, Constantes.D_CN);
                            dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_JP, Constantes.HighHappiness_JP, Constantes.D_JP);
                        }
                        else if (dataJson.FR.WhenEvolution.Contains(Constantes.N_FR))
                        {
                            dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_EN, Constantes.HighHappiness_EN, Constantes.N_EN);
                            dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_ES, Constantes.HighHappiness_ES, Constantes.N_ES);
                            dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_IT, Constantes.HighHappiness_IT, Constantes.N_IT);
                            dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_DE, Constantes.HighHappiness_DE, Constantes.N_DE);
                            dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_RU, Constantes.HighHappiness_RU, Constantes.N_RU);
                            dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_CO, Constantes.HighHappiness_CO, Constantes.N_CO);
                            dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_CN, Constantes.HighHappiness_CN, Constantes.N_CN);
                            dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_2_JP, Constantes.HighHappiness_JP, Constantes.N_JP);
                        }
                        else
                        {
                            dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_EN, Constantes.HighHappiness_EN);
                            dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_ES, Constantes.HighHappiness_ES);
                            dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_IT, Constantes.HighHappiness_IT);
                            dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_DE, Constantes.HighHappiness_DE);
                            dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_RU, Constantes.HighHappiness_RU);
                            dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_CO, Constantes.HighHappiness_CO);
                            dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_CN, Constantes.HighHappiness_CN);
                            dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpWith_Format_JP, Constantes.HighHappiness_JP);
                        }

                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.InTeam_FR))
                    {
                        getTranslateWithUrl(Constantes.Remoraid_URL, dicTranslationLvlUp, true);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.InTeam_Format_EN, dicTranslationLvlUp[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.InTeam_Format_ES, dicTranslationLvlUp[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.InTeam_Format_IT, dicTranslationLvlUp[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.InTeam_Format_DE, dicTranslationLvlUp[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.InTeam_Format_RU, dicTranslationLvlUp[Constantes.RU]);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.InTeam_Format_CO, dicTranslationLvlUp[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.InTeam_Format_CN, dicTranslationLvlUp[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.InTeam_Format_JP, dicTranslationLvlUp[Constantes.JP]);
                    }
                    #endregion
                    break;
                case Constantes.LvlUpLearn_FR:
                    #region LvlUpLearn
                    Dictionary<string, string> dicTranslationLvlUpLearn = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.MimicOutsideGalar_FR))
                    {
                        getTranslateWithUrl(Constantes.Mimic_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]) + "(" + Constantes.OSG_EN + ")";
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]) + "(" + Constantes.OSG_ES + ")";
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]) + "(" + Constantes.OSG_IT + ")";
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]) + "(" + Constantes.OSG_DE + ")";
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Mimic_RU) + "(" + Constantes.OSG_RU + ")";
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]) + "(" + Constantes.OSG_CO + ")";
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]) + "(" + Constantes.OSG_CN + ")";
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]) + "(" + Constantes.OSG_JP + ")";
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Mimic_FR))
                    {
                        getTranslateWithUrl(Constantes.Mimic_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Mimic_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Double_Hit_FR))
                    {
                        getTranslateWithUrl(Constantes.Double_Hit_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Double_Hit_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Rollout_FR))
                    {
                        getTranslateWithUrl(Constantes.Rollout_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Rollout_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Ancient_Power_FR))
                    {
                        getTranslateWithUrl(Constantes.Ancient_Power_URL, dicTranslationLvlUpLearn, true);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Ancient_Power_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Stomp_FR))
                    {
                        getTranslateWithUrl(Constantes.Stomp_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Stomp_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Dragon_Pulse_FR))
                    {
                        getTranslateWithUrl(Constantes.Dragon_Pulse_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Dragon_Pulse_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Taunt_FR))
                    {
                        getTranslateWithUrl(Constantes.Taunt_URL, dicTranslationLvlUpLearn);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_EN, dicTranslationLvlUpLearn[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_ES, dicTranslationLvlUpLearn[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_IT, dicTranslationLvlUpLearn[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_DE, dicTranslationLvlUpLearn[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_RU, Constantes.Taunt_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CO, dicTranslationLvlUpLearn[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_CN, dicTranslationLvlUpLearn[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.LvlUpLearn_Format_JP, dicTranslationLvlUpLearn[Constantes.JP]);
                    }
                    #endregion
                    break;
                case Constantes.LvlUpWH_FR:
                    #region LvlUpWH
                    Dictionary<string, string> dicTranslationLvlUpWH = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.Oval_Stone_FR))
                    {
                        getTranslateWithUrl(Constantes.Oval_Stone_URL, dicTranslationLvlUpWH);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_EN, dicTranslationLvlUpWH[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_ES, dicTranslationLvlUpWH[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_IT, dicTranslationLvlUpWH[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_DE, dicTranslationLvlUpWH[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_RU, Constantes.Oval_Stone_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_CO, dicTranslationLvlUpWH[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_CN, dicTranslationLvlUpWH[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Oval_Stone_Format_JP, dicTranslationLvlUpWH[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Razor_Claw_FR))
                    {
                        getTranslateWithUrl(Constantes.Razor_Claw_URL, dicTranslationLvlUpWH);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Razor_Format_EN, dicTranslationLvlUpWH[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Razor_Format_ES, dicTranslationLvlUpWH[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Razor_Format_IT, dicTranslationLvlUpWH[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Razor_Format_DE, dicTranslationLvlUpWH[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Razor_Format_RU, Constantes.Razor_Claw_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Razor_Format_CO, dicTranslationLvlUpWH[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Razor_Format_CN, dicTranslationLvlUpWH[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Razor_Format_JP, dicTranslationLvlUpWH[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Razor_Fang_FR))
                    {
                        getTranslateWithUrl(Constantes.Razor_Fang_URL, dicTranslationLvlUpWH);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.Razor_Format_EN, dicTranslationLvlUpWH[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.Razor_Format_ES, dicTranslationLvlUpWH[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.Razor_Format_IT, dicTranslationLvlUpWH[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.Razor_Format_DE, dicTranslationLvlUpWH[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.Razor_Format_RU, Constantes.Razor_Fang_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.Razor_Format_CO, dicTranslationLvlUpWH[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.Razor_Format_CN, dicTranslationLvlUpWH[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.Razor_Format_JP, dicTranslationLvlUpWH[Constantes.JP]);
                    }
                    #endregion
                    break;
                default:
                    #region Default
                    Dictionary<string, string> dicTranslationDefault = new();
                    if (dataJson.FR.WhenEvolution.Contains(Constantes.Galarica_Cuff_FR))
                    {
                        getTranslateWithUrl(Constantes.Galarica_Cuff_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.UseA_Format_EN, dicTranslationDefault[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.UseA_Format_ES, dicTranslationDefault[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.UseA_Format_IT, dicTranslationDefault[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.UseA_Format_DE, dicTranslationDefault[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.UseA_Format_RU, Constantes.Galarica_Cuff_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.UseA_Format_CO, dicTranslationDefault[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.UseA_Format_CN, dicTranslationDefault[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.UseA_Format_JP, dicTranslationDefault[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Galarica_Wreath_FR))
                    {
                        getTranslateWithUrl(Constantes.Galarica_Wreath_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.UseA_Format_EN, dicTranslationDefault[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.UseA_Format_ES, dicTranslationDefault[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.UseA_Format_IT, dicTranslationDefault[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.UseA_Format_DE, dicTranslationDefault[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.UseA_Format_RU, Constantes.Galarica_Wreath_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.UseA_Format_CO, dicTranslationDefault[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.UseA_Format_CN, dicTranslationDefault[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.UseA_Format_JP, dicTranslationDefault[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Phione_FR))
                    {
                        getTranslateWithUrl(Constantes.Phione_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = String.Format(Constantes.NotEvolve_Format_EN, dicTranslationDefault[Constantes.EN]);
                        dataJson.ES.WhenEvolution = String.Format(Constantes.NotEvolve_Format_ES, dicTranslationDefault[Constantes.ES]);
                        dataJson.IT.WhenEvolution = String.Format(Constantes.NotEvolve_Format_IT, dicTranslationDefault[Constantes.IT]);
                        dataJson.DE.WhenEvolution = String.Format(Constantes.NotEvolve_Format_DE, dicTranslationDefault[Constantes.DE]);
                        dataJson.RU.WhenEvolution = String.Format(Constantes.NotEvolve_Format_RU, Constantes.Phione_RU);
                        dataJson.CO.WhenEvolution = String.Format(Constantes.NotEvolve_Format_CO, dicTranslationDefault[Constantes.CO]);
                        dataJson.CN.WhenEvolution = String.Format(Constantes.NotEvolve_Format_CN, dicTranslationDefault[Constantes.CN]);
                        dataJson.JP.WhenEvolution = String.Format(Constantes.NotEvolve_Format_JP, dicTranslationDefault[Constantes.JP]);
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.LvlUpLanakila_FR))
                    {
                        dataJson.EN.WhenEvolution = Constantes.LvlUpLanakila_EN;
                        dataJson.ES.WhenEvolution = Constantes.LvlUpLanakila_ES;
                        dataJson.IT.WhenEvolution = Constantes.LvlUpLanakila_IT;
                        dataJson.DE.WhenEvolution = Constantes.LvlUpLanakila_DE;
                        dataJson.RU.WhenEvolution = Constantes.LvlUpLanakila_RU;
                        dataJson.CO.WhenEvolution = Constantes.LvlUpLanakila_CO;
                        dataJson.CN.WhenEvolution = Constantes.LvlUpLanakila_CN;
                        dataJson.JP.WhenEvolution = Constantes.LvlUpLanakila_JP;
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Tart_Apple_FR))
                    {
                        getTranslateWithUrl(Constantes.Tart_Apple_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = dicTranslationDefault[Constantes.EN];
                        dataJson.ES.WhenEvolution = dicTranslationDefault[Constantes.ES];
                        dataJson.IT.WhenEvolution = dicTranslationDefault[Constantes.IT];
                        dataJson.DE.WhenEvolution = dicTranslationDefault[Constantes.DE];
                        dataJson.RU.WhenEvolution = Constantes.Tart_Apple_RU;
                        dataJson.CO.WhenEvolution = dicTranslationDefault[Constantes.CO];
                        dataJson.CN.WhenEvolution = dicTranslationDefault[Constantes.CN];
                        dataJson.JP.WhenEvolution = dicTranslationDefault[Constantes.JP];
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Sweet_Apple_FR))
                    {
                        getTranslateWithUrl(Constantes.Sweet_Apple_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = dicTranslationDefault[Constantes.EN];
                        dataJson.ES.WhenEvolution = dicTranslationDefault[Constantes.ES];
                        dataJson.IT.WhenEvolution = dicTranslationDefault[Constantes.IT];
                        dataJson.DE.WhenEvolution = dicTranslationDefault[Constantes.DE];
                        dataJson.RU.WhenEvolution = Constantes.Sweet_Apple_RU;
                        dataJson.CO.WhenEvolution = dicTranslationDefault[Constantes.CO];
                        dataJson.CN.WhenEvolution = dicTranslationDefault[Constantes.CN];
                        dataJson.JP.WhenEvolution = dicTranslationDefault[Constantes.JP];
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Cracked_Pot_FR))
                    {
                        getTranslateWithUrl(Constantes.Cracked_Pot_URL, dicTranslationDefault);

                        dataJson.EN.WhenEvolution = dicTranslationDefault[Constantes.EN];
                        dataJson.ES.WhenEvolution = dicTranslationDefault[Constantes.ES];
                        dataJson.IT.WhenEvolution = dicTranslationDefault[Constantes.IT];
                        dataJson.DE.WhenEvolution = dicTranslationDefault[Constantes.DE];
                        dataJson.RU.WhenEvolution = Constantes.Cracked_Pot_RU;
                        dataJson.CO.WhenEvolution = dicTranslationDefault[Constantes.CO];
                        dataJson.CN.WhenEvolution = dicTranslationDefault[Constantes.CN];
                        dataJson.JP.WhenEvolution = dicTranslationDefault[Constantes.JP];
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Critical_Hits_FR))
                    {
                        dataJson.EN.WhenEvolution = Constantes.Critical_Hits_EN;
                        dataJson.ES.WhenEvolution = Constantes.Critical_Hits_ES;
                        dataJson.IT.WhenEvolution = Constantes.Critical_Hits_IT;
                        dataJson.DE.WhenEvolution = Constantes.Critical_Hits_DE;
                        dataJson.RU.WhenEvolution = Constantes.Critical_Hits_RU;
                        dataJson.CO.WhenEvolution = Constantes.Critical_Hits_CO;
                        dataJson.CN.WhenEvolution = Constantes.Critical_Hits_CN;
                        dataJson.JP.WhenEvolution = Constantes.Critical_Hits_JP;
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.Mr_Mime))
                    {
                        dataJson.FR.WhenEvolution = Constantes.Mr_Mime_FR;
                        dataJson.EN.WhenEvolution = Constantes.Mr_Mime_EN;
                        dataJson.ES.WhenEvolution = Constantes.Mr_Mime_ES;
                        dataJson.IT.WhenEvolution = Constantes.Mr_Mime_IT;
                        dataJson.DE.WhenEvolution = Constantes.Mr_Mime_DE;
                        dataJson.RU.WhenEvolution = Constantes.Mr_Mime_RU;
                        dataJson.CO.WhenEvolution = Constantes.Mr_Mime_CO;
                        dataJson.CN.WhenEvolution = Constantes.Mr_Mime_CN;
                        dataJson.JP.WhenEvolution = Constantes.Mr_Mime_JP;
                    }
                    else if (dataJson.FR.WhenEvolution.Contains(Constantes.SavageLands_FR))
                    {
                        dataJson.EN.WhenEvolution = Constantes.SavageLands_EN;
                        dataJson.ES.WhenEvolution = Constantes.SavageLands_ES;
                        dataJson.IT.WhenEvolution = Constantes.SavageLands_IT;
                        dataJson.DE.WhenEvolution = Constantes.SavageLands_DE;
                        dataJson.RU.WhenEvolution = Constantes.SavageLands_RU;
                        dataJson.CO.WhenEvolution = Constantes.SavageLands_CO;
                        dataJson.CN.WhenEvolution = Constantes.SavageLands_CN;
                        dataJson.JP.WhenEvolution = Constantes.SavageLands_JP;
                    }
                    #endregion
                    break;
            }
        }

        public static Dictionary<string, string> getTranslateWithUrl(string url, Dictionary<string, string> dicTranslation, bool RU = false)
        {
            string response = HttpClientUtils.CallUrl(url).Result;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
                
            //EN
            HtmlNode value = htmlDoc.DocumentNode.Descendants("h1").First(node => node.GetAttributeValue("lang", "").Contains("en"));
            dicTranslation.Add(Constantes.EN, value.InnerText.Replace("(Pokémon)", ""));
            //ES
            value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.ES_Lib));
            dicTranslation.Add(Constantes.ES, value.InnerText.Split("\n")[3]);
            //IT
            value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.IT_Lib));
            dicTranslation.Add(Constantes.IT, value.InnerText.Split("\n")[3]);
            //DE
            value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.DE_Lib));
            dicTranslation.Add(Constantes.DE, value.InnerText.Split("\n")[3]);
            //RU
            if (RU)
            {
                try
                {
                    value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.RU_Lib));
                }
                catch
                {
                    value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("More languages")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.RU_Lib));
                }

                dicTranslation.Add(Constantes.RU, value.InnerText.Split("\n")[3].Split(" ")[0]);
            }
            //CO
            value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.CO_Lib));
            dicTranslation.Add(Constantes.CO, value.InnerText.Split("\n")[3].Split(" ")[0]);
            //CN
            value = htmlDoc.DocumentNode.Descendants("table").Last(node => node.InnerHtml.Contains("Language")).Descendants("tr").Last(node => node.InnerHtml.Contains(Constantes.CN_Lib));
            dicTranslation.Add(Constantes.CN, value.InnerText.Split("\n")[3].Split(" ")[0]);

            //JP
            try
            {
                value = htmlDoc.DocumentNode.Descendants("span").First(node => node.GetAttributeValue("lang", "").Contains("ja"));
            }
            catch
            {
                value = htmlDoc.DocumentNode.Descendants("span").First(node => node.GetAttributeValue("class", "").Contains("explain"));
            }
            dicTranslation.Add(Constantes.JP, value.InnerText);

            return dicTranslation;
        }
    }
}
