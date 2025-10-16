using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class PokemonDto : IdentityDto
    {
        public string? Number { get; set; }

        public DataInfoDto DataInfo { get; set; }

        public List<TypePokDto>? TypePoks { get; set; }
        public List<TypePokDto>? Weaknesses { get; set; }
        public List<Pokemon_TalentDto>? Talents { get; set; }
        public List<Pokemon_AttaqueDto>? Attaques { get; set; }

        public string? TypeEvolution { get; set; }

        public int StatPv { get; set; }

        public int StatAttaque { get; set; }

        public int StatDefense { get; set; }

        public int StatAttaqueSpe { get; set; }

        public int StatDefenseSpe { get; set; }

        public int StatVitesse { get; set; }

        public int StatTotal { get; set; }

        public string? EggMoves { get; set; }

        public string? CaptureRate { get; set; }

        public string? BasicHappiness { get; set; }

        public int Generation { get; set; }

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

        public GameDto Game { get; set; }
        
        public PokemonDto()
        {
            DataInfo = new();
            
            TypePoks = new ();
            Weaknesses = new ();
            Talents = new ();
            Attaques = new ();
            Game = new();
        }
    }
}
