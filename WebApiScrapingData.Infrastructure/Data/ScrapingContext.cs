using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContext : IdentityDbContext<IdentityUser>, IUnitOfWork
    {
        #region Constructor
        public ScrapingContext(DbContextOptions<ScrapingContext> options) : base(options)
        {
            //Use without Migration
            //Database.EnsureCreated();
        }
        #endregion

        #region Internal Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Appel de la base d'abord pour que Identity crée ses tables
            base.OnModelCreating(builder);

            // 🔒 Par défaut, toutes les relations ont un DeleteBehavior.Restrict
            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // ✅ Exception : si on supprime un utilisateur, on supprime aussi ses RefreshTokens
            builder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //// ⚙️ Configuration explicite pour Pokemon_EvolvesTo (évite l'ambiguïté entre PokemonId et EvolveToId)
            builder.Entity<Pokemon_EvolvesTo>()
                .HasOne(e => e.Pokemon)
                .WithMany()
                .HasForeignKey(e => e.PokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Pokemon_EvolvesTo>()
                .HasOne(e => e.EvolveTo)
                .WithMany()
                .HasForeignKey(e => e.EvolveToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion

        #region Properties
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Pokemon> Pokemons { get; set; }
        public virtual DbSet<DataInfo> DataInfos { get; set; }
        public virtual DbSet<TypePok> TypesPok { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<Attack> Attacks { get; set; }
        public virtual DbSet<TypeAttack> TypeAttacks { get; set; }
        public virtual DbSet<Pokemon_TypePok> Pokemon_TypePok { get; set; }
        public virtual DbSet<Pokemon_Weakness> Pokemon_Weakness { get; set; }
        public virtual DbSet<Pokemon_Ability> Pokemon_Ability { get; set; }
        public virtual DbSet<Pokemon_Attack> Pokemon_Attack { get; set; }
        public virtual DbSet<Pokemon_EvolvesTo> Pokemon_EvolveTo { get; set; }

        public virtual DbSet<Pokemon_EvolutionChain> Pokemon_EvolutionChain { get; set; }
        public virtual DbSet<EvolutionChain> EvolutionChain { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Quizz> Quizzs { get; set; }
        public virtual DbSet<Quizz_Question> Quizz_Question { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Question_Answer> Question_Answer { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuizzDifficulty> QuizzDifficulties { get; set; }
        #endregion

        #region Public Methods
        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
        
        public TEntity GetEntityById<TEntity>(DbSet<TEntity> dbSet, int id) where TEntity : class, ITIdentity
        {
            return dbSet.Single(x => x.Id.Equals(id));
        }

        public DbSet<T>? GetDbSetByName<T>(string entityTypeName, ScrapingContext context) where T : class
        {
            var entityType = GetEntityTypeByName(entityTypeName);

            if (entityType == null)
            {
                throw new ArgumentException($"Type {entityTypeName} not found.");
            }

            // Utilisation de la réflexion pour obtenir le DbSet correspondant
            try
            {
                var method = typeof(ScrapingContext).GetMethod(nameof(GetDbSet));
                var genericMethod = method?.MakeGenericMethod(entityType);

                Console.WriteLine(genericMethod.Invoke(context, null));
                try
                {
                    var dbSet = genericMethod.Invoke(context, null) as DbSet<T>;

                    if (dbSet is not null)
                    {
                        // Faire quelque chose avec dbSet...
                        return dbSet as DbSet<T>;
                    }
                    return dbSet;
                }
                catch (Exception ex)
                {
                    // Gérer l'exception
                    Console.WriteLine($"Erreur lors de l'invocation de la méthode : {ex.Message}");
                    
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Type {entityTypeName} not found.", ex);
            }
        }
        
        public Type GetEntityTypeByName(string entityTypeName)
        {
            // Charger l'assembly du projet où se trouve la classe
            Assembly assembly = Assembly.Load("WebApiScrapingData.Domain");

            // Utilisation de la réflexion pour obtenir le type de la classe par son nom
            return assembly.GetTypes().FirstOrDefault(t => t.Name == entityTypeName);
        }
        #endregion
    }
}