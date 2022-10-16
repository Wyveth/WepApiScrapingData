using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
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
        #endregion
    }
}