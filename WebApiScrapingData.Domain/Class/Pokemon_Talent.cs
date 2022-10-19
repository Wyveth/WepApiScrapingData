using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
    }
}
