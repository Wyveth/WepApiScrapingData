using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.Controllers
{
    
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypeAttaqueController : ControllerBase
    {
        #region Fields
        private readonly IRepository<TypeAttaque> _repository;
        private readonly IRepository<TypePok> _repositoryTP;
        private readonly IRepository<Attaque> _repositoryA;
        #endregion

        #region Constructors
        public TypeAttaqueController(IRepository<TypeAttaque> repository, IRepository<TypePok> repositoryTP, IRepository<Attaque> repositoryA)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryA = repositoryA;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<TypeAttaque>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<TypeAttaque> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<TypeAttaque> GetFindByName(string name)
        {
            return _repository.Find(m => m.Name_FR.Equals(name));
        }

        [HttpPut]
        [Route("UpdateTypeAttaqueInDB")]
        public async Task UpdateTypeAttaqueInDB()
        {
            List<TypeAttaque> typeAttaques = new List<TypeAttaque>();

            TypeAttaque typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Physical_Name_FR;
            typeAttaque.Description_FR = Constantes.Physical_Description_FR;
            typeAttaque.Name_EN = Constantes.Physical_Name_EN;
            typeAttaque.Description_EN = Constantes.Physical_Description_EN;
            typeAttaque.Name_ES = Constantes.Physical_Name_ES;
            typeAttaque.Description_ES = Constantes.Physical_Description_ES;
            typeAttaque.Name_IT = Constantes.Physical_Name_IT;
            typeAttaque.Description_IT = Constantes.Physical_Description_IT;
            typeAttaque.Name_DE = Constantes.Physical_Name_DE;
            typeAttaque.Description_DE = Constantes.Physical_Description_DE;
            typeAttaque.Name_RU = Constantes.Physical_Name_RU;
            typeAttaque.Description_RU = Constantes.Physical_Description_RU;
            typeAttaque.Name_CO = Constantes.Physical_Name_CO;
            typeAttaque.Description_CO = Constantes.Physical_Description_CO;
            typeAttaque.Name_CN = Constantes.Physical_Name_CN;
            typeAttaque.Description_CN = Constantes.Physical_Description_CN;
            typeAttaque.Name_JP = Constantes.Physical_Name_JP;
            typeAttaque.Description_JP = Constantes.Physical_Description_JP;
            typeAttaque.UrlImg = Constantes.Physical_UrlImg;
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Special_Name_FR;
            typeAttaque.Description_FR = Constantes.Special_Description_FR;
            typeAttaque.Name_EN = Constantes.Special_Name_EN;
            typeAttaque.Description_EN = Constantes.Special_Description_EN;
            typeAttaque.Name_ES = Constantes.Special_Name_ES;
            typeAttaque.Description_ES = Constantes.Special_Description_ES;
            typeAttaque.Name_IT = Constantes.Special_Name_IT;
            typeAttaque.Description_IT = Constantes.Special_Description_IT;
            typeAttaque.Name_DE = Constantes.Special_Name_DE;
            typeAttaque.Description_DE = Constantes.Special_Description_DE;
            typeAttaque.Name_RU = Constantes.Special_Name_RU;
            typeAttaque.Description_RU = Constantes.Special_Description_RU;
            typeAttaque.Name_CO = Constantes.Special_Name_CO;
            typeAttaque.Description_CO = Constantes.Special_Description_CO;
            typeAttaque.Name_CN = Constantes.Special_Name_CN;
            typeAttaque.Description_CN = Constantes.Special_Description_CN;
            typeAttaque.Name_JP = Constantes.Special_Name_JP;
            typeAttaque.Description_JP = Constantes.Special_Description_JP;
            typeAttaque.UrlImg = Constantes.Special_UrlImg;
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = Constantes.Status_Name_FR;
            typeAttaque.Description_FR = Constantes.Status_Description_FR;
            typeAttaque.Name_EN = Constantes.Status_Name_EN;
            typeAttaque.Description_EN = Constantes.Status_Description_EN;
            typeAttaque.Name_ES = Constantes.Status_Name_ES;
            typeAttaque.Description_ES = Constantes.Status_Description_ES;
            typeAttaque.Name_IT = Constantes.Status_Name_IT;
            typeAttaque.Description_IT = Constantes.Status_Description_IT;
            typeAttaque.Name_DE = Constantes.Status_Name_DE;
            typeAttaque.Description_DE = Constantes.Status_Description_DE;
            typeAttaque.Name_RU = Constantes.Status_Name_RU;
            typeAttaque.Description_RU = Constantes.Status_Description_RU;
            typeAttaque.Name_CO = Constantes.Status_Name_CO;
            typeAttaque.Description_CO = Constantes.Status_Description_CO;
            typeAttaque.Name_CN = Constantes.Status_Name_CN;
            typeAttaque.Description_CN = Constantes.Status_Description_CN;
            typeAttaque.Name_JP = Constantes.Status_Name_JP;
            typeAttaque.Description_JP = Constantes.Status_Description_JP;
            typeAttaque.UrlImg = Constantes.Status_UrlImg;
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            await _repository.AddRange(typeAttaques);
            _repository.UnitOfWork.SaveChanges();
        }

        [HttpPut]
        [Route("DlUpdatePathUrl")]
        public async Task DlUpdatePathUrl()
        {
            var httpClient = new HttpClient();
            IEnumerable<TypeAttaque> typeAttaques = await _repository.GetAll();
            foreach (TypeAttaque typeAttaque in typeAttaques)
            {
                StringBuilder nameBuilder = new StringBuilder();
                switch (typeAttaque.Name_FR)
                {
                    case Constantes.Physical_Name_FR:
                        nameBuilder.Append(Constantes.Physical);
                        break;
                    case Constantes.Special_Name_FR:
                        nameBuilder.Append(Constantes.Special);
                        break;
                    case Constantes.Status_Name_FR:
                        nameBuilder.Append(Constantes.Status);
                        break;
                }
                typeAttaque.PathImg = await HttpClientUtils.DownloadTypeAttackFileTaskAsync(httpClient, typeAttaque.UrlImg, nameBuilder.ToString());
            }
            
            _repository.UnitOfWork.SaveChanges();

            httpClient.Dispose();
        }
    }
}
