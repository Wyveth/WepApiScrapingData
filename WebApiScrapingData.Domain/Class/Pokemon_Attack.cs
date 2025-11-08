using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_Attack: Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        [DataMember]
        public virtual Pokemon? Pokemon { get; set; }

        public long AttackId { get; set; }
        [ForeignKey("AttackId")]
        [DataMember]
        public virtual Attack? Attack { get; set; }

        [DataMember]
        public string? TypeLearn { get; set; }

        [DataMember]
        public string? Level { get; set; }

        [DataMember]
        public string? CTCS { get; set; }
    }
}
