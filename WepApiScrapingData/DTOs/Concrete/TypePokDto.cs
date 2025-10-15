using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class TypePokDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? PathMiniHome { get; set; }

        public string? PathMiniGo { get; set; }

        public string? PathFondGo { get; set; }

        public string? PathIconHome { get; set; }

        public string? PathAutoHome { get; set; }
        
        public string? ImgColor { get; set; }

        public string? InfoColor { get; set; }

        public string? TypeColor { get; set; }
    }
}
