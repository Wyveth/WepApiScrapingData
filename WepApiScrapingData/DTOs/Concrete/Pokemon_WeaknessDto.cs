using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class Pokemon_WeaknessDto : IdentityDto
    {
        public long PokemonId { get; set; }

        public long TypePokId { get; set; }
    }
}
