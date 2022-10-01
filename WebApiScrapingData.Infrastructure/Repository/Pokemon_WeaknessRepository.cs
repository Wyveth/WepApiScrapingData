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
    public class Pokemon_WeaknessRepository : IRepository<Pokemon_Weakness>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public Pokemon_WeaknessRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public void Add(Pokemon_Weakness entity)
        {
            UpdateInfo(entity);
            this._context.Pokemon_Weakness.Add(entity);
        }

        public void AddRange(IEnumerable<Pokemon_Weakness> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            this._context.Pokemon_Weakness.AddRange(entities);
        }

        public void SaveJsonInDb(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read
        public IEnumerable<Pokemon_Weakness> Find(Expression<Func<Pokemon_Weakness, bool>> predicate)
        {
            return this._context.Pokemon_Weakness.Where(predicate ?? (s => true)).AsQueryable();
        }

        public Pokemon_Weakness Get(int id)
        {
            return this._context.Pokemon_Weakness.Single(x => x.Id.Equals(id));
        }

        public IEnumerable<Pokemon_Weakness> GetAll()
        {
            return this._context.Pokemon_Weakness.AsQueryable();
        }
        #endregion

        #region Update
        public void Edit(Pokemon_Weakness entity)
        {
            UpdateInfo(entity, true);
            this._context.Pokemon_Weakness.Update(entity);
        }

        public void EditRange(IEnumerable<Pokemon_Weakness> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.Pokemon_Weakness.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(Pokemon_Weakness entity)
        {
            this._context.Pokemon_Weakness.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Pokemon_Weakness> entities)
        {
            this._context.Pokemon_Weakness.RemoveRange(entities);
        }

        public Pokemon_Weakness? SingleOrDefault(Expression<Func<Pokemon_Weakness, bool>> predicate)
        {
            return this._context.Pokemon_Weakness.SingleOrDefault(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        private void UpdateInfo(Pokemon_Weakness entity, bool edit = false)
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
