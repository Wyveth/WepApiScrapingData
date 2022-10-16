using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiScrapingData.Infrastructure.Loggers
{
    public class CustomLogger : ILogger
    {
        #region Fields
        private readonly string _name;
        private readonly Func<CustomLoggerConfiguration> _getCurrentConfig;
        #endregion

        #region Constructor
        public CustomLogger(string name, Func<CustomLoggerConfiguration> getCurrentConfig) => (_name, _getCurrentConfig) = (name, getCurrentConfig);
        #endregion

        #region¨Public Methods
        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            CustomLoggerConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = Console.ForegroundColor;

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.WriteLine($"[{DateTime.Now}][{eventId.Id,2}: {logLevel,-12}]");

                Console.ForegroundColor = originalColor;
                Console.Write($"     {_name} - ");

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.Write($"{formatter(state, exception)}");

                Console.ForegroundColor = originalColor;
                Console.WriteLine();
            }
        }
        #endregion
    }
}
