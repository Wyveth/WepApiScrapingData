using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class AnswerRepository : IRepository<Answer>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public AnswerRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Answer entity)
        {
            UpdateInfo(entity);
            await this._context.Answers.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Answer> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Answers.AddRangeAsync(entities);
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Answer> Find(Expression<Func<Answer, bool>> predicate)
        {
            return this._context.Answers.Where(predicate).AsQueryable();
        }

        public async Task<Answer> Get(int id)
        {
            return await this._context.Answers.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Answer> Query()
        {
            return this._context.Answers.AsQueryable();
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await this._context.Answers.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Answer entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.Answers.Update(entity);
        }

        public void EditRange(IEnumerable<Answer> entities)
        {
            this._context.Answers.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Answer entity)
        {
            this._context.Answers.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Answer> entities)
        {
            this._context.Answers.RemoveRange(entities);
        }

        public async Task<Answer?> SingleOrDefault(Expression<Func<Answer, bool>> predicate)
        {
            return await this._context.Answers.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Answer answer, bool edit = false)
        {
            answer.UserModification = "System";
            answer.DateModification = DateTime.Now;

            if (!edit)
            {
                answer.UserCreation = "System";
                answer.DateCreation = DateTime.Now;
                answer.versionModification = 1;
            }
            else
                answer.versionModification += 1;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
