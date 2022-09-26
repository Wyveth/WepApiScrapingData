using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Framework;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository
{
    public class DataInfoRepository : IRepository<DataInfo>
    {
        #region Fields
        private readonly ScrapingContext _context;
        #endregion

        #region Constructor
        public DataInfoRepository(ScrapingContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public void Add(DataInfo entity)
        {
            entity.UserCreation = "System";
            entity.DateCreation = DateTime.Now;
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification = 1;
            
            this._context.DataInfos.Add(entity);
        }

        public DataInfo AddInPokemon(DataInfo entity)
        {
            entity.UserCreation = "System";
            entity.DateCreation = DateTime.Now;
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification = 1;

            return this._context.DataInfos.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<DataInfo> entities)
        {
            foreach (DataInfo entity in entities)
                this.Add(entity);
        }

        public void SaveJsonInDb(string json)
        {
            List<DataInfoJson> dataInfosJson = JsonConvert.DeserializeObject<List<DataInfoJson>>(json);
            foreach (DataInfoJson dataInfoJson in dataInfosJson)
            {
                DataInfo dataInfo = new();
                this.MapToInstance(dataInfo, dataInfoJson);
                this.Add(dataInfo);
            }
        }
        
        public DataInfo SaveJsonInDb(DataInfoJson dataInfoJson)
        {
            DataInfo dataInfo = new();
            this.MapToInstance(dataInfo, dataInfoJson);
            return this.AddInPokemon(dataInfo);
        }
        #endregion

        #region Read
        public IEnumerable<DataInfo> Find(Expression<Func<DataInfo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DataInfo Get(int id)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<DataInfo> GetAll()
        {
            return this._context.DataInfos.AsQueryable();
        }
        #endregion

        #region Update
        public void Edit(DataInfo entity)
        {
            entity.UserModification = "System";
            entity.DateModification = DateTime.Now;
            entity.versionModification += 1;

            this._context.DataInfos.Update(entity);
        }

        public void EditRange(IEnumerable<DataInfo> entities)
        {
            foreach (DataInfo entity in entities)
                this.Edit(entity);
        }
        #endregion

        #region Delete
        public void Remove(DataInfo entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<DataInfo> entities)
        {
            throw new NotImplementedException();
        }

        public DataInfo SingleOrDefault(Expression<Func<DataInfo, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Private Methods
        public void MapToInstance(DataInfo dataInfo, DataInfoJson dataInfoJson)
        {
            dataInfo.Name = dataInfoJson.Name;
            dataInfo.DisplayName = dataInfoJson.DisplayName;
            dataInfo.DescriptionVx = dataInfoJson.DescriptionVx;
            dataInfo.DescriptionVy = dataInfoJson.DescriptionVy;
            dataInfo.Size = dataInfoJson.Size;
            dataInfo.Category = dataInfoJson.Category;
            dataInfo.Weight = dataInfoJson.Weight;
            dataInfo.Talent = dataInfoJson.Talent;
            dataInfo.DescriptionTalent = dataInfoJson.DescriptionTalent;
            dataInfo.Types = dataInfoJson.Types;
            dataInfo.Weakness = dataInfoJson.Weakness;
            dataInfo.Evolutions = dataInfoJson.Evolutions;
            dataInfo.WhenEvolution = dataInfoJson.WhenEvolution;
            dataInfo.NextUrl = dataInfoJson.NextUrl;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
