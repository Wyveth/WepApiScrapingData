using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class AttaqueRepository : IRepository<Attaque>
    {
        #region Fields        
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public AttaqueRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion
        
        #region Public Methods
        #region Create
        public async Task Add(Attaque entity)
        {
            UpdateInfo(entity);
            await this._context.Attaques.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Attaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Attaques.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Attaque> Find(Expression<Func<Attaque, bool>> predicate)
        {
            return this._context.Attaques.Include("typeAttaque").Include("typePok").Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Attaque> Get(int id)
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").SingleAsync(x => x.Id.Equals(id));
        }

        public async Task<Attaque> GetByName(string name)
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<Attaque> Query()
        {
            return this._context.Attaques.Include("typeAttaque").Include("typePok").AsQueryable();
        }

        public async Task<IEnumerable<Attaque>> GetAll()
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Attaque entity)
        {
            UpdateInfo(entity, true);
            this._context.Attaques.Update(entity);
        }

        public void EditRange(IEnumerable<Attaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Attaques.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Attaque entity)
        {
            this._context.Attaques.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Attaque> entities)
        {
            this._context.Attaques.RemoveRange(entities);
        }

        public async Task<Attaque?> SingleOrDefault(Expression<Func<Attaque, bool>> predicate)
        {
            return await this._context.Attaques.SingleOrDefaultAsync(predicate);
        }
        #endregion

        #region Private Methods
        private void UpdateInfo(Attaque entity, bool edit = false)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;

            if (!edit)
            {
                entity.UserCreation = "System";
                entity.DateCreation = DateTime.Now;
                entity.versionModification = 1;
            }
            else
                entity.versionModification += 1;
        }
        #endregion
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
