using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class TypePokLightDto : IdentityDto
    {
        public string? Name { get; set; }

        public string? PathMiniHome { get; set; }
    }
}
