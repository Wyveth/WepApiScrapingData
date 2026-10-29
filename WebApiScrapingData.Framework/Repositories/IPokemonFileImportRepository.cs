namespace WebApiScrapingData.Core.Repositories;

public interface IPokemonFileImportRepository
{
    Task SaveJsonInDb(string json);
    Task SaveInfoPokemonAttackInDB(string json);
}
