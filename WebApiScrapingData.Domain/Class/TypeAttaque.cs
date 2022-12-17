using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class TypeAttaque :Identity
    {
        [DataMember(Name = DataMember.Name_FR)]
        public string? Name_FR { get; set; }

        [DataMember(Name = DataMember.Description_FR)]
        public string? Description_FR { get; set; }

        [DataMember(Name = DataMember.Name_EN)]
        public string? Name_EN { get; set; }

        [DataMember(Name = DataMember.Description_EN)]
        public string? Description_EN { get; set; }

        [DataMember(Name = DataMember.Name_ES)]
        public string? Name_ES { get; set; }

        [DataMember(Name = DataMember.Description_ES)]
        public string? Description_ES { get; set; }

        [DataMember(Name = DataMember.Name_IT)]
        public string? Name_IT { get; set; }

        [DataMember(Name = DataMember.Description_IT)]
        public string? Description_IT { get; set; }

        [DataMember(Name = DataMember.Name_DE)]
        public string? Name_DE { get; set; }

        [DataMember(Name = DataMember.Description_DE)]
        public string? Description_DE { get; set; }

        [DataMember(Name = DataMember.Name_RU)]
        public string? Name_RU { get; set; }

        [DataMember(Name = DataMember.Description_RU)]
        public string? Description_RU { get; set; }

        [DataMember(Name = DataMember.Name_CO)]
        public string? Name_CO { get; set; }

        [DataMember(Name = DataMember.Description_CO)]
        public string? Description_CO { get; set; }

        [DataMember(Name = DataMember.Name_CN)]
        public string? Name_CN { get; set; }

        [DataMember(Name = DataMember.Description_CN)]
        public string? Description_CN { get; set; }

        [DataMember(Name = DataMember.Name_JP)]
        public string? Name_JP { get; set; }

        [DataMember(Name = DataMember.Description_JP)]
        public string? Description_JP { get; set; }

        //Url de l'Image
        public string? UrlImg { get; set; }

        //Url de l'Image Interne
        [DataMember(Name = DataMember.UrlImg)]
        public string? PathImg { get; set; }
    }
}
