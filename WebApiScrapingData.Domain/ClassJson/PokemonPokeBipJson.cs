namespace WebApiScrapingData.Domain.ClassJson
{
    [Serializable]
    public class PokemonPokeBipJson
    {
        //Numéro du Pokémon
        public string? Number { get; set; }

        public string? Name { get; set; }
        public string? DisplayName { get; set; }

        public string? Specie { get; set; }
        
        public string? EggMoves { get; set; }
        
        public string? CaptureRate { get; set; }
        
        public string? BasicHappiness { get; set; }
        
        public string? HiddenSkill { get; set; }

        public List<AttackJson> AttackJsons { get; set; }

        public string? NextUrl { get; set; }

        public PokemonPokeBipJson()
        {
            AttackJsons = new List<AttackJson>();
        }
    }
}
