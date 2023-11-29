using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class TypePokDto : IdentityDto
    {
        public string? Name_FR { get; set; }

        public string? PathMiniHome_FR { get; set; }

        public string? Name_EN { get; set; }

        public string? PathMiniHome_EN { get; set; }

        public string? Name_ES { get; set; }

        public string? PathMiniHome_ES { get; set; }

        public string? Name_IT { get; set; }

        public string? PathMiniHome_IT { get; set; }

        public string? Name_DE { get; set; }

        public string? PathMiniHome_DE { get; set; }

        public string? Name_RU { get; set; }

        public string? PathMiniHome_RU { get; set; }

        public string? Name_CO { get; set; }

        public string? PathMiniHome_CO { get; set; }

        public string? Name_CN { get; set; }

        public string? PathMiniHome_CN { get; set; }

        public string? Name_JP { get; set; }
        
        public string? PathMiniHome_JP { get; set; }

        public string? UrlMiniHome { get; set; }

        public string? UrlMiniGo { get; set; }
        public string? PathMiniGo { get; set; }

        public string? UrlFondGo { get; set; }
        public string? PathFondGo { get; set; }

        public string? UrlIconHome { get; set; }
        public string? PathIconHome { get; set; }

        public string? UrlAutoHome { get; set; }
        public string? PathAutoHome { get; set; }
        
        public string? ImgColor { get; set; }

        public string? InfoColor { get; set; }

        public string? TypeColor { get; set; }

        public List<long>? Pokemon_TypePoks { get; set; }

        public List<long>? Pokemon_Weaknesses { get; set; }
    }
}
