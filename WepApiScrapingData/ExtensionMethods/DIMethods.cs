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
            services.AddScoped<TypeAttackMapper>();
            services.AddScoped<TypePokMapper>();
            services.AddScoped<Pokemon_AbilityMapper>();
            services.AddScoped<AbilityMapper>();
            services.AddScoped<Pokemon_AttackMapper>();
            services.AddScoped<AttackMapper>();
            services.AddScoped<PokemonMapper>();

            services.AddScoped<GenericMapper<Game, GameDto>, GameMapper>();
            services.AddScoped<GenericMapper<TypeAttack, TypeAttackDto>, TypeAttackMapper>();
            services.AddScoped<GenericMapper<TypePok, TypePokDto>, TypePokMapper>();
            services.AddScoped<GenericMapper<Pokemon_Ability, Pokemon_AbilityDto>, Pokemon_AbilityMapper>();
            services.AddScoped<GenericMapper<Ability, AbilityDto>, AbilityMapper>();
            services.AddScoped<GenericMapper<Pokemon_Attack, Pokemon_AttackDto>, Pokemon_AttackMapper>();
            services.AddScoped<GenericMapper<Attack, AttackDto>, AttackMapper>();
            services.AddScoped<GenericMapper<Pokemon, PokemonDto>, PokemonMapper>();

            services.AddScoped<GameRepository>();
            services.AddScoped<AttackRepository>();
            services.AddScoped<DataInfoRepository>();
            services.AddScoped<PokemonRepository>();
            services.AddScoped<AbilityRepository>();
            services.AddScoped<TypeAttackRepository>();
            services.AddScoped<TypePokRepository>();
            services.AddScoped<EvolutionChainRepository>();
            services.AddScoped<Pokemon_TypePokRepository>();
            services.AddScoped<Pokemon_WeaknessRepository>();
            services.AddScoped<Pokemon_AbilityRepository>();
            services.AddScoped<Pokemon_AttackRepository>();
            services.AddScoped<Pokemon_EvolvesToRepository>();

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
