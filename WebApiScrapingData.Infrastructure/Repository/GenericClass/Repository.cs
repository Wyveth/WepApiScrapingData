using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, ITIdentity
    {
        #region Fields        
        protected readonly ScrapingContext _context;
        #endregion

        public Repository(ScrapingContext context)
        {
            _context = context;
        }

        #region Public Methods
        #region Read
        public async Task<TEntity?> Get(int id)
        {
            return await _context.Set<TEntity>().SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate ?? (s => true)).ToListAsync();
        }

        public async Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
        #endregion

        #region Create
        public async Task Add(TEntity entity)
        {
            UpdateInfo(entity);
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Edit(TEntity entity)
        {
            UpdateInfo(entity, true);
            _context.Set<TEntity>().Update(entity);
        }

        public void EditRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);
            _context.Set<TEntity>().UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        #endregion

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void UpdateInfo(TEntity entity, bool edit = false)
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
        public IUnitOfWork UnitOfWork => _context;
        #endregion
    }
}
