using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class Pokemon_AttaqueRepository : IRepository<Pokemon_Attaque>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public Pokemon_AttaqueRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Pokemon_Attaque entity)
        {
            UpdateInfo(entity);
            await this._context.Pokemon_Attaque.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Pokemon_Attaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Pokemon_Attaque.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Pokemon_Attaque> Find(Expression<Func<Pokemon_Attaque, bool>> predicate)
        {
            return this._context.Pokemon_Attaque.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Pokemon_Attaque> Get(int id)
        {
            return await this._context.Pokemon_Attaque.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Pokemon_Attaque> Query()
        {
            return this._context.Pokemon_Attaque.AsQueryable();
        }

        public async Task<IEnumerable<Pokemon_Attaque>> GetAll()
        {
            return await this._context.Pokemon_Attaque.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Pokemon_Attaque entity)
        {
            UpdateInfo(entity, true);
            this._context.Pokemon_Attaque.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon_Attaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Pokemon_Attaque.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Pokemon_Attaque entity)
        {
            this._context.Pokemon_Attaque.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Pokemon_Attaque> entities)
        {
            this._context.Pokemon_Attaque.RemoveRange(entities);
        }

        public async Task<Pokemon_Attaque?> SingleOrDefault(Expression<Func<Pokemon_Attaque, bool>> predicate)
        {
            return await this._context.Pokemon_Attaque.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Pokemon_Attaque entity, bool edit = false)
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

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
