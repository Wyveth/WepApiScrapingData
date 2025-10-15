using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class PokemonLightDto : IdentityDto
    {
        public string? Number { get; set; }

        public DataInfoDto DataInfo { get; set; }

        public List<TypePokDto>? TypePoks { get; set; }

        public string? PathImgLegacy { get; set; }

        public string? PathImgNormal { get; set; }

        public string? PathImgShiny { get; set; }

        public string? PathSpriteLegacy { get; set; }

        public string? PathSpriteNormal { get; set; }

        public string? PathSpriteShiny { get; set; }

        public string? PathSound { get; set; }

        public string? PathSoundLegacy { get; set; }

        public string? PathSoundCurrent { get; set; }

        public string? PathAnimatedImg { get; set; }

        public string? PathAnimatedImgShiny { get; set; }

        public PokemonLightDto()
        {
            DataInfo = new();
            TypePoks = new();
        }
    }
}
