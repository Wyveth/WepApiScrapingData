using WebApiScrapingData.Infrastructure.Configurations;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.ExtensionMethods
{
    /// <summary>
    /// Custom options form Config (json)
    /// </summary>
    public static class OptionsMethods
    {
        #region Public Methods
        /// <summary>
        /// Add Custom Options Form Config
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection(Constantes.SecurityOption));
            services.Configure<CorsOption>(configuration.GetSection(Constantes.CorsOption));
            services.Configure<WepApiScrapingData.Services.Game.DuelOptions>(configuration.GetSection(WepApiScrapingData.Services.Game.DuelOptions.Section));
            services.Configure<WepApiScrapingData.Services.Game.HangmanOptions>(configuration.GetSection(WepApiScrapingData.Services.Game.HangmanOptions.Section));
        }
        #endregion
    }
}
