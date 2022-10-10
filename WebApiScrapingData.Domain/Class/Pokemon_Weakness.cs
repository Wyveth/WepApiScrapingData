﻿using System;
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
    public class Pokemon_Weakness : Identity
    {
        public long PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }

        public long TypePokId { get; set; }
        [ForeignKey("TypePokId")]
        [DataMember]
        public TypePok TypePok { get; set; }
    }
}
