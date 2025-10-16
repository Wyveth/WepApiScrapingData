using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Mapper;
using WebApiScrapingData.Infrastructure.Repository;
using WebApiScrapingData.Infrastructure.Repository.Class;
using WebApiScrapingData.Infrastructure.Repository.Quizz;
using WepApiScrapingData.DTOs.Concrete;
using WepApiScrapingData.Mapper;

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
            services.AddScoped<GameMapper>();
            services.AddScoped<TypeAttaqueMapper>();
            services.AddScoped<TypePokMapper>();
            services.AddScoped<Pokemon_TalentMapper>();
            services.AddScoped<TalentMapper>();
            services.AddScoped<Pokemon_AttaqueMapper>();
            services.AddScoped<AttaqueMapper>();
            services.AddScoped<PokemonMapper>();

            services.AddScoped<GenericMapper<Game, GameDto>, GameMapper>();
            services.AddScoped<GenericMapper<TypeAttaque, TypeAttaqueDto>, TypeAttaqueMapper>();
            services.AddScoped<GenericMapper<TypePok, TypePokDto>, TypePokMapper>();
            services.AddScoped<GenericMapper<Pokemon_Talent, Pokemon_TalentDto>, Pokemon_TalentMapper>();
            services.AddScoped<GenericMapper<Talent, TalentDto>, TalentMapper>();
            services.AddScoped<GenericMapper<Pokemon_Attaque, Pokemon_AttaqueDto>, Pokemon_AttaqueMapper>();
            services.AddScoped<GenericMapper<Attaque, AttaqueDto>, AttaqueMapper>();
            services.AddScoped<GenericMapper<Pokemon, PokemonDto>, PokemonMapper>();

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
            services.AddScoped<Quizz_QuestionRepository>();
            services.AddScoped<QuestionRepository>();
            services.AddScoped<Question_AnswerRepository>();
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
