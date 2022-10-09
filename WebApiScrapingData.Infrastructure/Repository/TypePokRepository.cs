using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Abstract;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class TypePokRepository : IRepository<TypePok>
    {
        #region Fields        
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public TypePokRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task Add(TypePok entity)
        {
            UpdateInfo(entity);
            await this._context.TypesPok.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TypePok> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity);

            await this._context.TypesPok.AddRangeAsync(entities);
        }

        public async Task SaveJsonInDb(string json)
        {
            List<TypePokJson> typesPokJson = JsonConvert.DeserializeObject<List<TypePokJson>>(json);
            foreach (TypePokJson typePokJson in typesPokJson)
            {
                TypePok typePok = new();
                this.MapToInstance(typePok, typePokJson);
                await this.Add(typePok);
            }
        }
        #endregion

        #region Read
        public IEnumerable<TypePok> Find(Expression<Func<TypePok, bool>> predicate)
        {
            return this._context.TypesPok.Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<TypePok> Get(int id)
        {
            return await this._context.TypesPok.SingleAsync(x => x.Id.Equals(id));
        }
        
        public async Task<TypePok> GetByName(string name)
        {
            return await this._context.TypesPok.SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<TypePok> Query()
        {
            return this._context.TypesPok.AsQueryable();
        }

        public async Task<IEnumerable<TypePok>> GetAll()
        {
            return await this._context.TypesPok.ToListAsync();
        }
        #endregion

        #region Update
        public void Edit(TypePok entity)
        {
            UpdateInfo(entity, true);
            this._context.TypesPok.Update(entity);
        }

        public void EditRange(IEnumerable<TypePok> entities)
        {
            foreach (var entity in entities)
                UpdateInfo(entity, true);

            this._context.TypesPok.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(TypePok entity)
        {
            this._context.TypesPok.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TypePok> entities)
        {
            this._context.TypesPok.RemoveRange(entities);
        }

        public async Task<TypePok?> SingleOrDefault(Expression<Func<TypePok, bool>> predicate)
        {
            return await this._context.TypesPok.SingleOrDefaultAsync(predicate);
        }
        #endregion
        #endregion

        #region Private Methods
        public void MapToInstance(TypePok typePok, TypePokJson typePokJson)
        {
            typePok.Name_FR = typePokJson.Name_FR;
            typePok.Name_EN = typePokJson.Name_EN;
            typePok.Name_ES = typePokJson.Name_ES;
            typePok.Name_IT = typePokJson.Name_IT;
            typePok.Name_DE = typePokJson.Name_DE;
            typePok.Name_RU = typePokJson.Name_RU;
            typePok.Name_CO = typePokJson.Name_CO;
            typePok.Name_CN = typePokJson.Name_CN;
            typePok.Name_JP = typePokJson.Name_JP;
            typePok.UrlMiniGo = typePokJson.UrlMiniGo;
            typePok.UrlFondGo = typePokJson.UrlFondGo;
            typePok.UrlMiniHome = typePokJson.UrlMiniHome;
            typePok.UrlIconHome = typePokJson.UrlIconHome;
            typePok.UrlAutoHome = typePokJson.UrlAutoHome;
            typePok.ImgColor = typePokJson.ImgColor;
            typePok.InfoColor = typePokJson.InfoColor;
            typePok.TypeColor = typePokJson.TypeColor;
        }

        private void UpdateInfo(TypePok entity, bool edit = false)
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
