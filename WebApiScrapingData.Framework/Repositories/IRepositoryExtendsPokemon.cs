using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Domain.Class.Quizz;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Core.Repositories.RepositoriesQuizz
{
    public interface IRepositoryExtendsPokemon<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllLight();
    }
}
