using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Repository.Generic;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.Repository.Class
{
    public class TalentRepository : Repository<Talent>
    {
        #region Constructor
        public TalentRepository(ScrapingContext context) : base(context) { }
        #endregion

        #region Public Methods
        #region Read
        public async Task<Talent> GetTalentRandom()
        {
            List<Talent> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);

            return await Task.FromResult(result[numberRandom]);
        }

        public async Task<Talent> GetTalentRandom(List<Talent> alreadySelected)
        {
            List<Talent> result = GetAll().Result.ToList();

            Random random = new Random();
            int numberRandom = random.Next(result.Count);
            Talent talent = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));

            while (talent != null)
            {
                numberRandom = random.Next(result.Count);
                talent = alreadySelected.Find(m => m.Id.Equals(result[numberRandom].Id));
            }

            return await Task.FromResult(result[numberRandom]);
        }
        #endregion
        #endregion
    }
}
