using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class FamilyDto : IdentityDto
    {
        public string? Number { get; set; }

        public DataInfoDto DataInfo { get; set; }

        public List<TypePokLightDto>? TypePoks { get; set; }

        public string TypeEvolution { get; set; }

        public string? WhenEvolution { get; set; }

        public string? PathImgNormal { get; set; }

        public string? PathSpriteNormal { get; set; }

        public FamilyDto()
        {
            DataInfo = new();
            TypePoks = new();
        }
    }
}
