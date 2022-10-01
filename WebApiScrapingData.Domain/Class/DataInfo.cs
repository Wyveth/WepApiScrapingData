using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    public class DataInfo : Identity
    {
        //Nom du Pokémon
        public string? Name { get; set; }

        //Nom Affiché
        public string? DisplayName { get; set; }

        //Description du Pokémon Version X
        public string? DescriptionVx { get; set; }

        //Description du Pokémon Version
        public string? DescriptionVy { get; set; }

        //Taille du Pokémon
        public string? Size { get; set; }

        //Catégorie du Pokémon
        public string? Category { get; set; }

        //Poids du Pokémon
        public string? Weight { get; set; }

        //Talent du Pokémon
        public string? Talent { get; set; }

        //Description du Talent
        public string? DescriptionTalent { get; set; }

        //Nom des Types
        public string? Types { get; set; }

        //Nom des Faiblesses
        public string? Weakness { get; set; }

        //Evolution/Famille du Pokémon
        public string? Evolutions { get; set; }

        //Savoir Quand où comment le pokémon évolue
        public string? WhenEvolution { get; set; }

        //Prochain Pokémon
        public string? NextUrl { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Pokemon? Pokemon { get; set; }
    }
}
