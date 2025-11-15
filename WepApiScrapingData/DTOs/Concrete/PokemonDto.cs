using WepApiScrapingData.DTOs.Abstract;

namespace WepApiScrapingData.DTOs.Concrete
{
    public class PokemonDto : IdentityDto
    {
        public string? Number { get; set; }

        public DataInfoDto DataInfo { get; set; }

        public List<TypePokDto>? TypePoks { get; set; }
        public List<TypePokDto>? Weaknesses { get; set; }
        public List<Pokemon_AbilityDto>? Abilities { get; set; }
        public List<Pokemon_AttackDto>? Attacks { get; set; }

        public string? TypeEvolution { get; set; }

        public List<Pokemon_EvolvesToDto>? EvolvesFrom { get; set; }

        public List<Pokemon_EvolvesToDto>? EvolvesTo { get; set; }

        public int EvolutionChainId { get; set; }

        public int? EvolutionStage { get; set; }

        public int StatPv { get; set; }

        public int StatAttack { get; set; }

        public int StatDefense { get; set; }

        public int StatAttackSpe { get; set; }

        public int StatDefenseSpe { get; set; }

        public int StatSpeed { get; set; }

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
            Abilities = new ();
            Attacks = new ();
            Game = new();

            EvolvesFrom = new();
            EvolvesTo = new ();
        }
    }
}
