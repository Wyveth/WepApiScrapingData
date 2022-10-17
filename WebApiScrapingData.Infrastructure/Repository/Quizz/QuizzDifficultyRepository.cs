using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuizzDifficultyRepository : IRepository<QuizzDifficulty>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public QuizzDifficultyRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(QuizzDifficulty entity)
        {
            UpdateInfo(entity);
            await this._context.QuizzDifficulties.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<QuizzDifficulty> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.QuizzDifficulties.AddRangeAsync(entities);
        }
        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<QuizzDifficulty> Find(Expression<Func<QuizzDifficulty, bool>> predicate)
        {
            return this._context.QuizzDifficulties.Where(predicate).AsQueryable();
        }

        public async Task<QuizzDifficulty> Get(int id)
        {
            return await this._context.QuizzDifficulties.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<QuizzDifficulty> Query()
        {
            return this._context.QuizzDifficulties.AsQueryable();
        }

        public async Task<IEnumerable<QuizzDifficulty>> GetAll()
        {
            return await this._context.QuizzDifficulties.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(QuizzDifficulty entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.QuizzDifficulties.Update(entity);
        }

        public void EditRange(IEnumerable<QuizzDifficulty> entities)
        {
            this._context.QuizzDifficulties.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(QuizzDifficulty entity)
        {
            this._context.QuizzDifficulties.Remove(entity);
        }

        public void RemoveRange(IEnumerable<QuizzDifficulty> entities)
        {
            this._context.QuizzDifficulties.RemoveRange(entities);
        }

        public async Task<QuizzDifficulty?> SingleOrDefault(Expression<Func<QuizzDifficulty, bool>> predicate)
        {
            return await this._context.QuizzDifficulties.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(QuizzDifficulty dataInfo, bool edit = false)
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
