using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Repository;

namespace WepApiScrapingData.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public Methods
        /// <summary>
        /// Prepare l'injection de dépendance custom
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Pokemon>, PokemonRepository>();
            services.AddScoped<IRepository<TypePok>, TypePokRepository>();
            
            return services;
        }
        #endregion
    }
}
