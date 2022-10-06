﻿using Newtonsoft.Json;
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
            this._context.DataInfos.AddRange(entities);
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
            return this._context.DataInfos.Where(predicate).AsQueryable();
        }

        public DataInfo Get(int id)
        {
            return this._context.DataInfos.Single(x => x.Id.Equals(id));
        }

        public IQueryable<DataInfo> Query()
        {
            return this._context.DataInfos.AsQueryable();
        }

        public IEnumerable<DataInfo> GetAll()
        {
            return this._context.DataInfos.ToList();
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
            this._context.DataInfos.UpdateRange(entities);
        }
        #endregion

        #region Delete
        public void Remove(DataInfo entity)
        {
            this._context.DataInfos.Remove(entity);
        }

        public void RemoveRange(IEnumerable<DataInfo> entities)
        {
            this._context.DataInfos.RemoveRange(entities);
        }

        public DataInfo? SingleOrDefault(Expression<Func<DataInfo, bool>> predicate)
        {
            return this._context.DataInfos.SingleOrDefault(predicate);
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