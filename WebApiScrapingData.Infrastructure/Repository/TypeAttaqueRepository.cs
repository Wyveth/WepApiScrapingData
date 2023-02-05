using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class TypeAttaqueRepository : IRepository<TypeAttaque>
    {
        #region Fields        
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public TypeAttaqueRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(TypeAttaque entity)
        {
            UpdateInfo(entity);
            await this._context.TypeAttaques.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TypeAttaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.TypeAttaques.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<TypeAttaque> Find(Expression<Func<TypeAttaque, bool>> predicate)
        {
            return this._context.TypeAttaques.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<TypeAttaque> Get(int id)
        {
            return await this._context.TypeAttaques.SingleAsync(x => x.Id.Equals(id));
        }
        
        public async Task<TypeAttaque> GetByName(string name)
        {
            return await this._context.TypeAttaques.SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<TypeAttaque> Query()
        {
            return this._context.TypeAttaques.AsQueryable();
        }

        public async Task<IEnumerable<TypeAttaque>> GetAll()
        {
            return await this._context.TypeAttaques.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(TypeAttaque entity)
        {
            UpdateInfo(entity, true);
            this._context.TypeAttaques.Update(entity);
        }

        public void EditRange(IEnumerable<TypeAttaque> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.TypeAttaques.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(TypeAttaque entity)
        {
            this._context.TypeAttaques.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TypeAttaque> entities)
        {
            this._context.TypeAttaques.RemoveRange(entities);
        }

        public async Task<TypeAttaque?> SingleOrDefault(Expression<Func<TypeAttaque, bool>> predicate)
        {
            return await this._context.TypeAttaques.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(TypeAttaque entity, bool edit = false)
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
