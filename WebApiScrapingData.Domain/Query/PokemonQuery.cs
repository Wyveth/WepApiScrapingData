using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Domain.Query
{
    public class PokemonQuery
    {
        public int Max { get; set; } = 20;
        public bool Limit { get; set; } = true;
        public int? Gen { get; set; }
        public bool Desc { get; set; } = false;
        public string Lang { get; set; } = "FR";
    }
}
