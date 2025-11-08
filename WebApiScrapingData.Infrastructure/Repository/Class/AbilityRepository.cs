using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class AbilityRepository : Repository<Ability>
    {
        #region Constructor
        public AbilityRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<Ability> GetByName(string name)
        {
            return await _context.Abilities.FirstOrDefaultAsync(x => x.Name_EN.Equals(name));
        }

        public async Task<Ability> GetAbilityRandom()
        {
            List<Ability> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);

            return await Task.FromResult(result[numberRandom]);
        }

        public async Task<Ability> GetAbilityRandom(List<Ability> alreadySelected)
        {
            List<Ability> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);
            Ability ability = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));

            while (ability != null)
            {
                numberRandom = random.Next(result.Count);
                ability = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));
            }

            return await Task.FromResult(result[numberRandom]);
        }
        #endregion
        #endregion
    }
}
