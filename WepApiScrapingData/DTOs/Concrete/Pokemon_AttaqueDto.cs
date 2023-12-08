using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class Pokemon_AttaqueDto : IdentityDto
    {
        public long PokemonId { get; set; }
        
        public long AttaqueId { get; set; }

        public string? TypeLearn { get; set; }

        public string? Level { get; set; }

        public string? CTCS { get; set; }
    }
}
