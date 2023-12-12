using Newtonsoft.Json;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class DataInfoRepository : Repository<DataInfo>
    {
        #region Constructor
        public DataInfoRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Create
        public async Task SaveJsonInDb(string json)
        {
            List<DataInfoJson> dataInfosJson = JsonConvert.DeserializeObject<List<DataInfoJson>>(json);
            foreach (DataInfoJson dataInfoJson in dataInfosJson)
            {
                DataInfo dataInfo = new();
                MapToInstance(dataInfo, dataInfoJson);
                await this.Add(dataInfo);
            }
        }

        public async Task<DataInfo> SaveJsonInDb(DataInfoJson dataInfoJson)
        {
            DataInfo dataInfo = new();
            MapToInstance(dataInfo, dataInfoJson);
            await Add(dataInfo);
            return await Task.FromResult(dataInfo);
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
    }
}