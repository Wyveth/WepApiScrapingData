﻿using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.Interface;

namespace WebApiScrapingData.Core.Repositories
{
    public interface IRepositoryExtendsPokemon<TEntity> : IRepository<TEntity> where TEntity : class, ITIdentity
    {
        Task<IEnumerable<Pokemon>> GetAllLight();

        Task<List<Pokemon>> GetFamilyWithoutVariantAsync(string family);

        Task<IEnumerable<Pokemon>> GetAllVariantAsync(string number);

        Task SaveInfoPokemonAttackInDB(string json);

        Task<bool> ImportJsonToDb(string json);
    }
}
