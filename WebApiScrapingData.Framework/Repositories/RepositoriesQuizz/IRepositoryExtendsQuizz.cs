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
    public interface IRepositoryExtendsQuizz<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GenerateQuizz(IdentityUser profile, bool gen1, bool gen2, bool gen3, bool gen4, bool gen5, bool gen6, bool gen7, bool gen8, bool genArceus, bool easy, bool normal, bool hard);
    }
}
