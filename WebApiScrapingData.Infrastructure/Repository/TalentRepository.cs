using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class TalentRepository : IRepository<Talent>
    {
        #region Fields        
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public TalentRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Talent entity)
        {
            UpdateInfo(entity);
            await this._context.Talents.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Talent> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Talents.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Talent> Find(Expression<Func<Talent, bool>> predicate)
        {
            return this._context.Talents.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Talent> Get(int id)
        {
            return await this._context.Talents.SingleAsync(x => x.Id.Equals(id));
        }
        
        public async Task<Talent> GetByName(string name)
        {
            return await this._context.Talents.SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<Talent> Query()
        {
            return this._context.Talents.AsQueryable();
        }

        public async Task<IEnumerable<Talent>> GetAll()
        {
            return await this._context.Talents.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Talent entity)
        {
            UpdateInfo(entity, true);
            this._context.Talents.Update(entity);
        }

        public void EditRange(IEnumerable<Talent> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Talents.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Talent entity)
        {
            this._context.Talents.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Talent> entities)
        {
            this._context.Talents.RemoveRange(entities);
        }

        public async Task<Talent?> SingleOrDefault(Expression<Func<Talent, bool>> predicate)
        {
            return await this._context.Talents.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Talent entity, bool edit = false)
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
