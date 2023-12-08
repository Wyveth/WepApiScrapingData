using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_Attaque: Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        [DataMember]
        public virtual Pokemon? Pokemon { get; set; }

        public long AttaqueId { get; set; }
        [ForeignKey("AttaqueId")]
        [DataMember]
        public virtual Attaque? Attaque { get; set; }

        [DataMember]
        public string? TypeLearn { get; set; }

        [DataMember]
        public string? Level { get; set; }

        [DataMember]
        public string? CTCS { get; set; }
    }
}
