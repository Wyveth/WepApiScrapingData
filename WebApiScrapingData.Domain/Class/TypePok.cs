using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    public class TypePok : Identity
    {
        //French Name
        public string? Name_FR { get; set; }

        //English Name
        public string? Name_EN { get; set; }

        //Spanish Name
        public string? Name_ES { get; set; }

        //Italian Name
        public string? Name_IT { get; set; }

        //German Name
        public string? Name_DE { get; set; }

        //Russian Name
        public string? Name_RU { get; set; }

        //Korean Name
        public string? Name_CO { get; set; }

        //Chinese Name
        public string? Name_CN { get; set; }

        //Japanese Name
        public string? Name_JP { get; set; }

        public string? UrlMiniGo { get; set; }
        public string? UrlFondGo { get; set; }
        public string? UrlMiniHome { get; set; }
        public string? UrlIconHome { get; set; }
        public string? UrlAutoHome { get; set; }

        public string? ImgColor { get; set; }
        public string? InfoColor { get; set; }
        public string? TypeColor { get; set; }

        [JsonIgnore]
        public List<Pokemon_TypePok> Pokemon_TypePoks { get; set; }
        
        [JsonIgnore]
        public List<Pokemon_Weakness> Pokemon_Weaknesses { get; set; }
    }
}
