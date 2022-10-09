using Microsoft.AspNetCore.Cors.Infrastructure;
using WebApiScrapingData.Infrastructure.Configurations;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.ExtensionMethods
{
    public static class SecurityMethods
    {
        #region Constantes
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Add Cors Configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomCors(configuration);
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOption corsOption = new CorsOption();
            configuration.GetSection(Constantes.CorsOption).Bind(corsOption);

            services.AddCors(options => {
                options.AddPolicy(name: DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
        #endregion
    }
}
