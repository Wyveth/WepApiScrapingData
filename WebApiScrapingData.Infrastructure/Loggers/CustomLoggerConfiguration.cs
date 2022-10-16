using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Infrastructure.Loggers
{
    public class CustomLoggerConfiguration
    {
        #region Fields
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {
            [LogLevel.Information] = ConsoleColor.Cyan,
            [LogLevel.Error] = ConsoleColor.DarkRed,
            [LogLevel.Warning] = ConsoleColor.Blue
        };
        #endregion
    }
}
