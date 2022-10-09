using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Infrastructure.Configurations
{
    /// <summary>
    /// Getting data to use in Cors Config
    /// </summary>
    public class CorsOption
    {
        #region Properties
        public string? Origin { get; set; }
        #endregion
    }
}
