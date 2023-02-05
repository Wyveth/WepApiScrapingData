using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class Pokemon_TalentRepository : IRepository<Pokemon_Talent>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public Pokemon_TalentRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Pokemon_Talent entity)
        {
            UpdateInfo(entity);
            await this._context.Pokemon_Talent.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Pokemon_Talent> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Pokemon_Talent.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Pokemon_Talent> Find(Expression<Func<Pokemon_Talent, bool>> predicate)
        {
            return this._context.Pokemon_Talent.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Pokemon_Talent> Get(int id)
        {
            return await this._context.Pokemon_Talent.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Pokemon_Talent> Query()
        {
            return this._context.Pokemon_Talent.AsQueryable();
        }

        public async Task<IEnumerable<Pokemon_Talent>> GetAll()
        {
            return await this._context.Pokemon_Talent.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Pokemon_Talent entity)
        {
            UpdateInfo(entity, true);
            this._context.Pokemon_Talent.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon_Talent> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Pokemon_Talent.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Pokemon_Talent entity)
        {
            this._context.Pokemon_Talent.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Pokemon_Talent> entities)
        {
            this._context.Pokemon_Talent.RemoveRange(entities);
        }

        public async Task<Pokemon_Talent?> SingleOrDefault(Expression<Func<Pokemon_Talent, bool>> predicate)
        {
            return await this._context.Pokemon_Talent.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Pokemon_Talent entity, bool edit = false)
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
