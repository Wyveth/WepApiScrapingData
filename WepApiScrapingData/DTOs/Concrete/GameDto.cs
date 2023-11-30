using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class GameDto : IdentityDto
    {
        public string? Name_FR { get; set; }
        public string? Name_EN { get; set; }
        public string? Name_ES { get; set; }
        public string? Name_IT { get; set; }
        public string? Name_DE { get; set; }
        public string? Name_RU { get; set; }
        public string? Name_CO { get; set; }
        public string? Name_CN { get; set; }
        public string? Name_JP { get; set; }
    }

}
