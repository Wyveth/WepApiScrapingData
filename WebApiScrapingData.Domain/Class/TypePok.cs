using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class TypePok : Identity
    {
        //French Name
        [DataMember(Name = DataMember.Name_FR)]
        public string? Name_FR { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_FR)]
        public string? PathMiniHome_FR { get; set; }

        //English Name
        [DataMember(Name = DataMember.Name_EN)]
        public string? Name_EN { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_EN)]
        public string? PathMiniHome_EN { get; set; }

        //Spanish Name
        [DataMember(Name = DataMember.Name_ES)]
        public string? Name_ES { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_ES)]
        public string? PathMiniHome_ES { get; set; }

        //Italian Name
        [DataMember(Name = DataMember.Name_IT)]
        public string? Name_IT { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_IT)]
        public string? PathMiniHome_IT { get; set; }

        //German Name
        [DataMember(Name = DataMember.Name_DE)]
        public string? Name_DE { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_DE)]
        public string? PathMiniHome_DE { get; set; }

        //Russian Name
        [DataMember(Name = DataMember.Name_RU)]
        public string? Name_RU { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_RU)]
        public string? PathMiniHome_RU { get; set; }

        //Korean Name
        [DataMember(Name = DataMember.Name_CO)]
        public string? Name_CO { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_CO)]
        public string? PathMiniHome_CO { get; set; }

        //Chinese Name
        [DataMember(Name = DataMember.Name_CN)]
        public string? Name_CN { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_CN)]
        public string? PathMiniHome_CN { get; set; }

        //Japanese Name
        [DataMember(Name = DataMember.Name_JP)]
        public string? Name_JP { get; set; }
        [DataMember(Name = DataMember.UrlMiniHome_JP)]
        public string? PathMiniHome_JP { get; set; }

        public string? UrlMiniHome { get; set; }

        public string? UrlMiniGo { get; set; }
        [DataMember(Name = DataMember.UrlMiniGo)]
        public string? PathMiniGo { get; set; }

        public string? UrlFondGo { get; set; }
        [DataMember(Name = DataMember.UrlFondGo)]
        public string? PathFondGo { get; set; }

        public string? UrlIconHome { get; set; }
        [DataMember(Name = DataMember.UrlIconHome)]
        public string? PathIconHome { get; set; }

        public string? UrlAutoHome { get; set; }
        [DataMember(Name = DataMember.UrlAutoHome)]
        public string? PathAutoHome { get; set; }

        [DataMember(Name = DataMember.ImgColor)]
        public string? ImgColor { get; set; }
        
        [DataMember(Name = DataMember.InfoColor)]
        public string? InfoColor { get; set; }
        
        [DataMember(Name = DataMember.TypeColor)]
        public string? TypeColor { get; set; }

        public List<Pokemon_TypePok>? Pokemon_TypePoks { get; set; }

        public List<Pokemon_Weakness>? Pokemon_Weaknesses { get; set; }
    }
}
