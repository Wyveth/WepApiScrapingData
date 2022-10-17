using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionAnswerRepository : IRepository<QuestionAnswer>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public QuestionAnswerRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(QuestionAnswer entity)
        {
            UpdateInfo(entity);
            await this._context.QuestionAnswers.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<QuestionAnswer> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.QuestionAnswers.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<QuestionAnswer> Find(Expression<Func<QuestionAnswer, bool>> predicate)
        {
            return this._context.QuestionAnswers.Where(predicate).AsQueryable();
        }

        public async Task<QuestionAnswer> Get(int id)
        {
            return await this._context.QuestionAnswers.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<QuestionAnswer> Query()
        {
            return this._context.QuestionAnswers.AsQueryable();
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAll()
        {
            return await this._context.QuestionAnswers.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(QuestionAnswer entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.QuestionAnswers.Update(entity);
        }

        public void EditRange(IEnumerable<QuestionAnswer> entities)
        {
            this._context.QuestionAnswers.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(QuestionAnswer entity)
        {
            this._context.QuestionAnswers.Remove(entity);
        }

        public void RemoveRange(IEnumerable<QuestionAnswer> entities)
        {
            this._context.QuestionAnswers.RemoveRange(entities);
        }

        public async Task<QuestionAnswer?> SingleOrDefault(Expression<Func<QuestionAnswer, bool>> predicate)
        {
            return await this._context.QuestionAnswers.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(QuestionAnswer dataInfo, bool edit = false)
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
