using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class.Quizz
{
    [DataContract]
    public class Difficulty : Identity
    {
        [DataMember(Name = DataMember.Code)]
        public string? Code { get; set; }
        
        [DataMember(Name = DataMember.Libelle_FR)]
        public string? Libelle_FR { get; set; }

        [DataMember(Name = DataMember.Libelle_EN)]
        public string? Libelle_EN { get; set; }

        [DataMember(Name = DataMember.Libelle_ES)]
        public string? Libelle_ES { get; set; }

        [DataMember(Name = DataMember.Libelle_IT)]
        public string? Libelle_IT { get; set; }

        [DataMember(Name = DataMember.Libelle_DE)]
        public string? Libelle_DE { get; set; }

        [DataMember(Name = DataMember.Libelle_RU)]
        public string? Libelle_RU { get; set; }

        [DataMember(Name = DataMember.Libelle_CO)]
        public string? Libelle_CO { get; set; }

        [DataMember(Name = DataMember.Libelle_CN)]
        public string? Libelle_CN { get; set; }

        [DataMember(Name = DataMember.Libelle_JP)]
        public string? Libelle_JP { get; set; }
    }
}
