using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class AttaqueRepository : Repository<Attaque>
    {
        #region Constructor
        public AttaqueRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public IEnumerable<Attaque> Find(Expression<Func<Attaque, bool>> predicate)
        {
            return this._context.Attaques.Include("typeAttaque").Include("typePok").Where(predicate ?? (s => true)).AsQueryable();
        }

        public async Task<Attaque> Get(int id)
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").SingleAsync(x => x.Id.Equals(id));
        }

        public async Task<Attaque> GetByName(string name)
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").SingleAsync(x => x.Name_FR.Equals(name));
        }

        public IQueryable<Attaque> Query()
        {
            return this._context.Attaques.Include("typeAttaque").Include("typePok").AsQueryable();
        }

        public async Task<IEnumerable<Attaque>> GetAll()
        {
            return await this._context.Attaques.Include("typeAttaque").Include("typePok").ToListAsync();
        }
        #endregion
        #endregion
    }
}
