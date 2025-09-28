using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class TypePokRepository : Repository<TypePok>
    {
        #region Constructor
        public TypePokRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Create
        public async Task SaveJsonInDb(string json)
        {
            List<TypePokMobileJsonV2> typesPokJson = JsonConvert.DeserializeObject<List<TypePokMobileJsonV2>>(json);
            foreach (TypePokMobileJsonV2 typePokJson in typesPokJson)
            {
                TypePok typePok = new();
                MapToInstance(typePok, typePokJson);
                await AddAsync(typePok);
            }
        }
        #endregion

        #region Read
        public async Task<TypePok> GetByName(string name)
        {
            return await _context.TypesPok.SingleAsync(x => x.Name_FR.Equals(name));
        }
        
        public Task<TypePok> GetTypeRandom()
        {
            List<TypePok> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);

            return Task.FromResult(result[numberRandom]);
        }

        public Task<TypePok> GetTypeRandom(List<TypePok> alreadySelected)
        {
            List<TypePok> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);
            TypePok typePok = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));

            while (typePok != null)
            {
                numberRandom = random.Next(result.Count);
                typePok = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));
            }

            return Task.FromResult(result[numberRandom]);
        }
        #endregion
        #endregion

        #region Private Methods
        public void MapToInstance(TypePok typePok, TypePokMobileJsonV2 typePokJson)
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
        #endregion
    }
}
