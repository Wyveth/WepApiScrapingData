using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Resources;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class EvolutionChain : Identity
    {
        [DataMember(Name = DataMember.Evolutions)]
        public string? Evolutions { get; set; } // Optionnel : liste de noms ou IDs
    }
}
