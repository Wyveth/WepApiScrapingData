using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_Ability : Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        [DataMember]
        public virtual Pokemon? Pokemon { get; set; }

        public long AbilityId { get; set; }
        [ForeignKey("AbilityId")]
        [DataMember]
        public virtual Ability? Ability { get; set; }

        [DataMember]
        public bool IsHidden { get; set; }
    }
}
