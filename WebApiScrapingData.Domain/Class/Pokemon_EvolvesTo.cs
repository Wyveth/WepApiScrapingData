using System.ComponentModel.DataAnnotations.Schema;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    public class Pokemon_EvolvesTo : Identity
    {
        [ForeignKey(nameof(Pokemon))]
        public long PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; } = null!; // Pokémon de base

        [ForeignKey(nameof(EvolveTo))]
        public long EvolveToId { get; set; }
        public virtual Pokemon EvolveTo { get; set; } = null!; // Pokémon vers lequel il évolue

        // Conditions multilingues
        public string? WhenEvolutionFR { get; set; }
        public string? WhenEvolutionEN { get; set; }
        public string? WhenEvolutionES { get; set; }
        public string? WhenEvolutionIT { get; set; }
        public string? WhenEvolutionDE { get; set; }
        public string? WhenEvolutionRU { get; set; }
        public string? WhenEvolutionCO { get; set; }
        public string? WhenEvolutionCN { get; set; }
        public string? WhenEvolutionJP { get; set; }
    }
}
