using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Generic
{
    public class Repository<T> : IRepository<T> where T : class, ITIdentity
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
        public virtual async Task<T?> Get(int id)
        {
            return await _context.Set<T>().SingleAsync(x => x.Id.Equals(id));
        }

        public virtual IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate ?? (s => true)).ToListAsync();
        }

        public virtual async Task<T?> SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
        #endregion

        #region Create        
        public async Task<bool> Add(T entity)
        {
            UpdateInfo(entity);
            await _context.Set<T>().AddAsync(entity);
            return saveChanges();
        }

        public async Task<bool> AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);
            await _context.Set<T>().AddRangeAsync(entities);
            return saveChanges();
        }
        #endregion

        #region Update
        public bool Update(T entity)
        {
            UpdateInfo(entity, true);
            _context.Set<T>().Update(entity);
            return saveChanges();
        }

        public bool UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);
            _context.Set<T>().UpdateRange(entities);
            return saveChanges();
        }
        #endregion

        #region Delete
        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            return saveChanges();
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return saveChanges();
        }
        #endregion

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void UpdateInfo(T entity, bool edit = false)
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

        private bool saveChanges()
        {
            // Enregistrez les modifications dans la base de données de manière synchrone
            int modifiedLines = _context.SaveChanges();

            // Vérifiez si l'opération a réussi en fonction du nombre de lignes modifiées
            return modifiedLines > 0;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => _context;
        #endregion
    }
}
