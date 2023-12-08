using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository.Quizz;

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
            services.AddScoped<DataInfoRepository>();
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
            services.AddScoped<Question_AnswerRepository>();
            services.AddScoped<QuestionAnswer_AnswerRepository>();
            services.AddScoped<Quizz_QuestionRepository>();
            services.AddScoped<QuestionRepository>();
            services.AddScoped<AnswerRepository>();
            services.AddScoped<QuestionTypeRepository>();
            services.AddScoped<QuizzDifficultyRepository>();
            services.AddScoped<DifficultyRepository>();
            services.AddScoped<QuestionAnswerRepository>();
            #endregion
            #endregion

            return services;
        }
        #endregion
    }
}
