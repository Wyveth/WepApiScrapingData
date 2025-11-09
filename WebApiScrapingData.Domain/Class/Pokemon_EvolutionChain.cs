using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_EvolutionChain : Identity
    {
        public long EvolutionChainId { get; set; }
        [ForeignKey("EvolutionChainId")]
        [DataMember]
        public virtual EvolutionChain EvolutionChain { get; set; } = null!;

        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        [DataMember]
        public virtual Pokemon? Pokemon { get; set; }
    }
}
