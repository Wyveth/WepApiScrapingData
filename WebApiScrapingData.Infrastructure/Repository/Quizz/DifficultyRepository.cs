using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class DifficultyRepository : IRepository<Difficulty>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public DifficultyRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Difficulty entity)
        {
            UpdateInfo(entity);
            await this._context.Difficulties.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Difficulty> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Difficulties.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Difficulty> Find(Expression<Func<Difficulty, bool>> predicate)
        {
            return this._context.Difficulties.Where(predicate).AsQueryable();
        }

        public async Task<Difficulty> Get(int id)
        {
            return await this._context.Difficulties.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Difficulty> Query()
        {
            return this._context.Difficulties.AsQueryable();
        }

        public async Task<IEnumerable<Difficulty>> GetAll()
        {
            return await this._context.Difficulties.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Difficulty entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.Difficulties.Update(entity);
        }

        public void EditRange(IEnumerable<Difficulty> entities)
        {
            this._context.Difficulties.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Difficulty entity)
        {
            this._context.Difficulties.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Difficulty> entities)
        {
            this._context.Difficulties.RemoveRange(entities);
        }

        public async Task<Difficulty?> SingleOrDefault(Expression<Func<Difficulty, bool>> predicate)
        {
            return await this._context.Difficulties.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Difficulty dataInfo, bool edit = false)
        {
            dataInfo.UserModification = "System";
            dataInfo.DateModification = DateTime.Now;

            if (!edit)
            {
                dataInfo.UserCreation = "System";
                dataInfo.DateCreation = DateTime.Now;
                dataInfo.versionModification = 1;
            }
            else
                dataInfo.versionModification += 1;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
