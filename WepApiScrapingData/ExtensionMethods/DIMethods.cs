using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
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
            services.AddScoped<IRepositoryExtendsPokemon<Pokemon>, PokemonRepository>();
            services.AddScoped<IRepository<TypePok>, TypePokRepository>();
            services.AddScoped<IRepository<Talent>, TalentRepository>();
            services.AddScoped<IRepository<Pokemon_TypePok>, Pokemon_TypePokRepository>();
            services.AddScoped<IRepository<Pokemon_Weakness>, Pokemon_WeaknessRepository>();
            services.AddScoped<IRepository<Pokemon_Talent>, Pokemon_TalentRepository>();

            #region Quizz
            services.AddScoped<IRepositoryExtendsQuizz<Quizz>, QuizzRepository>();
            services.AddScoped<IRepositoryExtendsQuestion<Question>, QuestionRepository>();
            services.AddScoped<IRepositoryExtendsAnswer<Answer>, AnswerRepository>();
            services.AddScoped<IRepository<QuestionType>, QuestionTypeRepository>();
            services.AddScoped<IRepository<QuizzDifficulty>, QuizzDifficultyRepository>();
            services.AddScoped<IRepository<Difficulty>, DifficultyRepository>();
            #endregion

            return services;
        }
        #endregion
    }
}
