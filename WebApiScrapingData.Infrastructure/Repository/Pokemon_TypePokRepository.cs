using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class Pokemon_TypePokRepository : IRepository<Pokemon_TypePok>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public Pokemon_TypePokRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public void Add(Pokemon_TypePok entity)
        {
            UpdateInfo(entity);
            this._context.Pokemon_TypePok.Add(entity);
        }

        public void AddRange(IEnumerable<Pokemon_TypePok> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            this._context.Pokemon_TypePok.AddRange(entities);
        }

        public void SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Pokemon_TypePok> Find(Expression<Func<Pokemon_TypePok, bool>> predicate)
        {
            return this._context.Pokemon_TypePok.Where(predicate ?? (s => true)).AsQueryable();
        }

        public Pokemon_TypePok Get(int id)
        {
            return this._context.Pokemon_TypePok.Single(x => x.Id.Equals(id));
        }

        public IEnumerable<Pokemon_TypePok> GetAll()
        {
            return this._context.Pokemon_TypePok.AsQueryable();
        }
        #endregion

        #region Update
        public void Edit(Pokemon_TypePok entity)
        {
            UpdateInfo(entity, true);
            this._context.Pokemon_TypePok.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon_TypePok> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Pokemon_TypePok.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Pokemon_TypePok entity)
        {
            this._context.Pokemon_TypePok.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Pokemon_TypePok> entities)
        {
            this._context.Pokemon_TypePok.RemoveRange(entities);
        }

        public Pokemon_TypePok? SingleOrDefault(Expression<Func<Pokemon_TypePok, bool>> predicate)
        {
            return this._context.Pokemon_TypePok.SingleOrDefault(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Pokemon_TypePok entity, bool edit = false)
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
