using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuestionRepository : IRepository<Question>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public QuestionRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Question entity)
        {
            UpdateInfo(entity);
            await this._context.Questions.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Question> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Questions.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Question> Find(Expression<Func<Question, bool>> predicate)
        {
            return this._context.Questions.Where(predicate).AsQueryable();
        }

        public async Task<Question> Get(int id)
        {
            return await this._context.Questions.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Question> Query()
        {
            return this._context.Questions.AsQueryable();
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await this._context.Questions.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Question entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.Questions.Update(entity);
        }

        public void EditRange(IEnumerable<Question> entities)
        {
            this._context.Questions.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Question entity)
        {
            this._context.Questions.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Question> entities)
        {
            this._context.Questions.RemoveRange(entities);
        }

        public async Task<Question?> SingleOrDefault(Expression<Func<Question, bool>> predicate)
        {
            return await this._context.Questions.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Question dataInfo, bool edit = false)
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
