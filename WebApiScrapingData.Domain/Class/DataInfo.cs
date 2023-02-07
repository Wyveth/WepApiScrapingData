using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class DataInfo : Identity
    {
        //Nom du Pokémon
        [DataMember(Name = DataMember.Name)]
        public string? Name { get; set; }

        //Nom Affiché
        [DataMember(Name = DataMember.DisplayName)]
        public string? DisplayName { get; set; }

        //Description du Pokémon Version X
        [DataMember(Name = DataMember.DescriptionVx)]
        public string? DescriptionVx { get; set; }

        //Description du Pokémon Version
        [DataMember(Name = DataMember.DescriptionVy)]
        public string? DescriptionVy { get; set; }

        //Taille du Pokémon
        [DataMember(Name = DataMember.Size)]
        public string? Size { get; set; }

        //Catégorie du Pokémon
        [DataMember(Name = DataMember.Category)]
        public string? Category { get; set; }

        //Poids du Pokémon
        [DataMember(Name = DataMember.Weight)]
        public string? Weight { get; set; }

        //Talent du Pokémon
        [DataMember(Name = DataMember.Talent)]
        public string? Talent { get; set; }

        //Description du Talent
        [DataMember(Name = DataMember.DescriptionTalent)]
        public string? DescriptionTalent { get; set; }

        //Nom des Types
        [DataMember(Name = DataMember.Types)]
        public string? Types { get; set; }

        //Nom des Faiblesses
        [DataMember(Name = DataMember.Weakness)]
        public string? Weakness { get; set; }

        //Evolution/Famille du Pokémon
        [DataMember(Name = DataMember.Evolutions)]
        public string? Evolutions { get; set; }

        //Savoir Quand où comment le pokémon évolue
        [DataMember(Name = DataMember.WhenEvolution)]
        public string? WhenEvolution { get; set; }

        //Prochain Pokémon
        [DataMember(Name = DataMember.NextUrl)]
        public string? NextUrl { get; set; }

        [NotMapped]
        public Pokemon? Pokemon { get; set; }
    }
}
