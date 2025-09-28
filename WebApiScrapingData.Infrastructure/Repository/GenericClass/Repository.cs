using Microsoft.EntityFrameworkCore;
using System;
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

        #region Constructor
        public Repository(ScrapingContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Read
        public virtual async Task<T?> Get(long id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<T?> GetByGuid(Guid guid)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Guid.Equals(guid));
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

        public virtual async Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleAsync(predicate);
        }

        public virtual async Task<T?> SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
        #endregion

        #region Create        
        public async Task<bool> AddAsync(T entity)
        {
            await UpdateInfoAsync(entity);
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                await UpdateInfoAsync(entity);

            await _context.Set<T>().AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateAsync(T entity)
        {
            await UpdateInfoAsync(entity, true);
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                await UpdateInfoAsync(entity, true);

            _context.Set<T>().UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Delete
        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0; // ✅ async save
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0; // ✅ async save
        }
        #endregion

        #region Import/Export
        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Private Methods
        private async Task UpdateInfoAsync(T entity, bool edit = false)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;

            if (!edit)
            {
                entity.Guid = await UpdateGuidAsync();  // ✅ await le GUID
                entity.UserCreation = "System";
                entity.DateCreation = DateTime.Now;
                entity.versionModification = 1;
            }
            else
            {
                entity.versionModification += 1;
            }
        }

        private async Task<Guid> UpdateGuidAsync()
        {
            Guid guid;
            bool guidOK = false;

            do
            {
                guid = Guid.NewGuid();
                var obj = await GetByGuid(guid);
                if (obj == null)
                    guidOK = true;

            } while (!guidOK);

            return guid;
        }

        private Guid UpdateGuid()
        {
            bool guidOK = false;
            Guid guid;
            do
            {
                guid = Guid.NewGuid();
                Task<T?> obj = GetByGuid(guid);
                if (obj.Result != null)
                    guidOK = true;

            } while (!guidOK);

            return guid;
        }

        private bool saveChanges()
        {
            // Enregistrez les modifications dans la base de données de manière synchrone
            int modifiedLines = _context.SaveChanges();

            // Vérifiez si l'opération a réussi en fonction du nombre de lignes modifiées
            return modifiedLines > 0;
        }

        private async Task<bool> SaveChangesAsync()
        {
            // Enregistrez les modifications dans la base de données de manière asynchrone
            int modifiedLines = await _context.SaveChangesAsync();

            // Vérifiez si l'opération a réussi en fonction du nombre de lignes modifiées
            return modifiedLines > 0;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => _context;
        #endregion
    }
}
