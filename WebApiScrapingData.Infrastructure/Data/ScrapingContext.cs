﻿using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContext : DbContext, IUnitOfWork
    {
        #region Constructor
        public ScrapingContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Internal Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Properties
        public virtual DbSet<Pokemon> Pokemons { get; set; }
        public virtual DbSet<DataInfo> DataInfos { get; set; }
        public virtual DbSet<TypePok> TypesPok { get; set; }
        #endregion
    }
}