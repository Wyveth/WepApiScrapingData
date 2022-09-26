using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Domain.Class;

namespace WebApiScrapingData.Framework
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<TypePok> TypesPok { get; set; }
        //IRepository<Pokemon> Pokemons { get; set; }
        int SaveChanges();
    }
}
