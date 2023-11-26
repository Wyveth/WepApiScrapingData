using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Core;

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
            services.AddScoped<Repository<Attaque>, AttaqueRepository>();
            services.AddScoped<Repository<Game>, GameRepository>();
            services.AddScoped<PokemonRepository>();
            services.AddScoped<Repository<Talent>, TalentRepository>();
            services.AddScoped<Repository<TypeAttaque>, TypeAttaqueRepository>();
            services.AddScoped<Repository<TypePok>, TypePokRepository>();
            services.AddScoped<Repository<Pokemon_TypePok>, Pokemon_TypePokRepository>();
            services.AddScoped<Repository<Pokemon_Weakness>, Pokemon_WeaknessRepository>();
            services.AddScoped<Repository<Pokemon_Talent>, Pokemon_TalentRepository>();
            services.AddScoped<Repository<Pokemon_Attaque>, Pokemon_AttaqueRepository>();

            #region Quizz
            services.AddScoped<Repository<Quizz>, QuizzRepository>();
            services.AddScoped<Repository<Question>, QuestionRepository>();
            services.AddScoped<Repository<Answer>, AnswerRepository>();
            services.AddScoped<Repository<QuestionType>, QuestionTypeRepository>();
            services.AddScoped<Repository<QuizzDifficulty>, QuizzDifficultyRepository>();
            services.AddScoped<Repository<Difficulty>, DifficultyRepository>();
            #endregion

            #region Controller
            services.AddScoped(typeof(GenericMapper<,>));
            services.AddScoped<GameRepository>();
            #endregion

            return services;
        }
        #endregion
    }
}
