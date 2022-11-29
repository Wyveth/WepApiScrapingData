using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Domain.Abstract;

namespace WebApiScrapingData.Domain.Class
{
    [DataContract]
    public class Pokemon_Attaque: Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public virtual Pokemon? Pokemon { get; set; }

        public long AttaqueId { get; set; }
        [ForeignKey("AttaqueId")]
        [DataMember]
        public virtual Attaque? Attaque { get; set; }
    }
}
