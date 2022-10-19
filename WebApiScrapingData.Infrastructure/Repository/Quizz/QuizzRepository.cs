using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class QuizzRepository : IRepositoryExtendsQuizz<Quizz>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public QuizzRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Quizz entity)
        {
            UpdateInfo(entity);
            await this._context.Quizzs.AddAsync(entity);
        }
        
        public async Task AddRange(IEnumerable<Quizz> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Quizzs.AddRangeAsync(entities);
        }
        #endregion

        #region Read
        public IEnumerable<Quizz> Find(Expression<Func<Quizz, bool>> predicate)
        {
            return this._context.Quizzs.Where(predicate).AsQueryable();
        }

        public async Task<Quizz> Get(int id)
        {
            return await this._context.Quizzs.SingleAsync(x => x.Id.Equals(id));
        }

        public IQueryable<Quizz> Query()
        {
            return this._context.Quizzs.AsQueryable();
        }

        public async Task<IEnumerable<Quizz>> GetAll()
        {
            return await this._context.Quizzs.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Quizz entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.Quizzs.Update(entity);
        }

        public void EditRange(IEnumerable<Quizz> entities)
        {
            this._context.Quizzs.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Quizz entity)
        {
            this._context.Quizzs.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Quizz> entities)
        {
            this._context.Quizzs.RemoveRange(entities);
        }

        public async Task<Quizz?> SingleOrDefault(Expression<Func<Quizz, bool>> predicate)
        {
            return await this._context.Quizzs.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Quizz dataInfo, bool edit = false)
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

        public Task<Quizz> GenerateQuizz(IdentityUser profile, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool genArceus, bool easy, bool normal, bool hard)
        {
            throw new NotImplementedException();
        }

        public Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
