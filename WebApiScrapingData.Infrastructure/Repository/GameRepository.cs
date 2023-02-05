using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class GameRepository : IRepository<Game>
    {
        #region Fields        
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public GameRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(Game entity)
        {
            UpdateInfo(entity);
            await this._context.Games.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<Game> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.Games.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Game> Find(Expression<Func<Game, bool>> predicate)
        {
            return this._context.Games.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Game> Get(int id)
        {
            return await this._context.Games.SingleAsync(x => x.Id.Equals(id));
        }
        
        public async Task<Game> GetByName(string name)
        {
            return await this._context.Games.SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<Game> Query()
        {
            return this._context.Games.AsQueryable();
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await this._context.Games.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(Game entity)
        {
            UpdateInfo(entity, true);
            this._context.Games.Update(entity);
        }

        public void EditRange(IEnumerable<Game> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Games.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Game entity)
        {
            this._context.Games.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Game> entities)
        {
            this._context.Games.RemoveRange(entities);
        }

        public async Task<Game?> SingleOrDefault(Expression<Func<Game, bool>> predicate)
        {
            return await this._context.Games.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Game entity, bool edit = false)
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
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
