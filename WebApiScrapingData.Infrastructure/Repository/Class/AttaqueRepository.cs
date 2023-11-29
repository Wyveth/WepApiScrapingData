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
        public override async Task<IEnumerable<Attaque>> Find(Expression<Func<Attaque, bool>> predicate)
        {
            return await this._context.Attaques.Include(m => m.typeAttaque).Include(m => m.typePok).Where(predicate ?? (s => true)).ToListAsync();
        }

        public override async Task<Attaque?> Get(int id)
        {
            return await this._context.Attaques.Include(m => m.typeAttaque).Include(m => m.typePok).SingleAsync(x => x.Id.Equals(id));
        }

        public async Task<Attaque?> GetByName(string name)
        {
            return await this._context.Attaques.Include(m => m.typeAttaque).Include(m => m.typePok).SingleAsync(x => x.Name_FR.Equals(name));
        }

        public override IQueryable<Attaque> Query()
        {
            return this._context.Attaques.Include(m => m.typeAttaque).Include(m => m.typePok).AsQueryable();
        }

        public override async Task<IEnumerable<Attaque>> GetAll()
        {
            return await this._context.Attaques.Include(m => m.typeAttaque).Include(m => m.typePok).ToListAsync();
        }
        #endregion
        #endregion
    }
}
