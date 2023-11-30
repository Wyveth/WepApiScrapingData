using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Mapper;

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
            #region Controller
            services.AddScoped(typeof(GenericMapper<,>));
            services.AddScoped<GameRepository>();
            services.AddScoped<AttaqueRepository>();
            services.AddScoped<PokemonRepository>();
            services.AddScoped<TalentRepository>();
            services.AddScoped<TypeAttaqueRepository>();
            services.AddScoped<TypePokRepository>();
            services.AddScoped<Pokemon_TypePokRepository>();
            services.AddScoped<Pokemon_WeaknessRepository>();
            services.AddScoped<Pokemon_TalentRepository>();
            services.AddScoped<Pokemon_AttaqueRepository>();

            #region Quizz
            services.AddScoped<QuizzRepository>();
            services.AddScoped<QuestionRepository>();
            services.AddScoped<AnswerRepository>();
            services.AddScoped<QuestionTypeRepository>();
            services.AddScoped<QuizzDifficultyRepository>();
            services.AddScoped<DifficultyRepository>();
            #endregion
            #endregion

            return services;
        }
        #endregion
    }
}
