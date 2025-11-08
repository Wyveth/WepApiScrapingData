using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Generic;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class AttackRepository : Repository<Attack>
    {
        #region Constructor
        public AttackRepository(ScrapingContext context) : base(context) { }
        #endregion
        
        #region Public Methods
        #region Read
        public override async Task<IEnumerable<Attack>> Find(Expression<Func<Attack, bool>> predicate)
        {
            return await this._context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).Where(predicate ?? (s => true)).ToListAsync();
        }

        public override async Task<Attack?> Get(long id)
        {
            return await _context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        
        public override async Task<Attack?> GetByGuid(Guid guid)
        {
            return await this._context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).FirstOrDefaultAsync(x => x.Guid.Equals(guid));
        }

        public async Task<Attack?> GetByName(string name)
        {
            return await this._context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).FirstOrDefaultAsync(x => x.Name_EN.Equals(name));
        }

        public override IQueryable<Attack> Query()
        {
            return this._context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).AsQueryable();
        }

        public override async Task<IEnumerable<Attack>> GetAll()
        {
            return await this._context.Attacks.Include(m => m.TypeAttack).Include(m => m.TypePok).ToListAsync();
        }
        #endregion
        #endregion
    }
}
