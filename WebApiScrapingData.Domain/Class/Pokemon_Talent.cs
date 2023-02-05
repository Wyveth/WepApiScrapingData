using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_Talent : Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public virtual Pokemon? Pokemon { get; set; }

        public long TalentId { get; set; }
        [ForeignKey("TalentId")]
        [DataMember]
        public virtual Talent? Talent { get; set; }

        [DataMember]
        public bool IsHidden { get; set; }
    }
}
