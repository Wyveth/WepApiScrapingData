using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContext : IdentityDbContext, IUnitOfWork
    {
        #region Constructor
        public ScrapingContext(DbContextOptions options) : base(options)
        {
            //Use without Migration
            //Database.EnsureCreated();
        }
        #endregion

        #region Internal Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
        #endregion

        #region Properties
        public virtual DbSet<Pokemon> Pokemons { get; set; }
        public virtual DbSet<DataInfo> DataInfos { get; set; }
        public virtual DbSet<TypePok> TypesPok { get; set; }
        public virtual DbSet<Pokemon_TypePok> Pokemon_TypePok { get; set; }
        public virtual DbSet<Pokemon_Weakness> Pokemon_Weakness { get; set; }

        public virtual DbSet<Quizz> Quizzs { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<QuizzDifficulty> QuizzDifficulties { get; set; }
        #endregion
    }
}