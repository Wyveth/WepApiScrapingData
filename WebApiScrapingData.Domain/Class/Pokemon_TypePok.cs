using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_TypePok : Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public virtual Pokemon? Pokemon { get; set; }

        public long TypePokId { get; set; }
        [ForeignKey("TypePokId")]
        [DataMember]
        public virtual TypePok? TypePok { get; set; }
    }
}
