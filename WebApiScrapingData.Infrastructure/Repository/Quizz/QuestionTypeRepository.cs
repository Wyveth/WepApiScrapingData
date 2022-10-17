using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionTypeRepository : IRepository<QuestionType>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public QuestionTypeRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(QuestionType entity)
        {
            UpdateInfo(entity);
            await this._context.QuestionTypes.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<QuestionType> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.QuestionTypes.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<QuestionType> Find(Expression<Func<QuestionType, bool>> predicate)
        {
            return this._context.QuestionTypes.Where(predicate).AsQueryable();
        }

        public async Task<QuestionType> Get(int id)
        {
            return await this._context.QuestionTypes.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<QuestionType> Query()
        {
            return this._context.QuestionTypes.AsQueryable();
        }

        public async Task<IEnumerable<QuestionType>> GetAll()
        {
            return await this._context.QuestionTypes.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(QuestionType entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.QuestionTypes.Update(entity);
        }

        public void EditRange(IEnumerable<QuestionType> entities)
        {
            this._context.QuestionTypes.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(QuestionType entity)
        {
            this._context.QuestionTypes.Remove(entity);
        }

        public void RemoveRange(IEnumerable<QuestionType> entities)
        {
            this._context.QuestionTypes.RemoveRange(entities);
        }

        public async Task<QuestionType?> SingleOrDefault(Expression<Func<QuestionType, bool>> predicate)
        {
            return await this._context.QuestionTypes.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(QuestionType dataInfo, bool edit = false)
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
