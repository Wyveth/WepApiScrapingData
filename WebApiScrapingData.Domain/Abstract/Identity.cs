using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Domain.Abstract
{
    public class Identity
    {
        [Key]
        public long Id { get; set; }

        public string UserCreation { get; set; } = "";

        public DateTime DateCreation { get; set; }

        public string UserModification { get; set; } = "";

        public DateTime DateModification { get; set; }

        public int versionModification { get; set; }
    }
}
