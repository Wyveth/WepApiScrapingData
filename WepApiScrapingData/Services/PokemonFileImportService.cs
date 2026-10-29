using WebApiScrapingData.Core.Repositories;

namespace WepApiScrapingData.Services;

public sealed class PokemonFileImportService
{
    private readonly string _contentRoot;
    private readonly IPokemonFileImportRepository _repository;

    public PokemonFileImportService(string contentRoot, IPokemonFileImportRepository repository)
    {
        _contentRoot = contentRoot;
        _repository = repository;
    }

    public async Task ImportPokemonAsync(int? generation = null)
    {
        var filename = generation.HasValue ? $"PokeScrapGen{generation.Value}.json" : "PokeScrap.json";
        var json = await File.ReadAllTextAsync(Path.Combine(_contentRoot, "PokeScrap", filename));
        await _repository.SaveJsonInDb(json);
    }

    public async Task ImportAttackInfoAsync()
    {
        var json = await File.ReadAllTextAsync(Path.Combine(_contentRoot, "PokeScrap", "PokeBipScrapGen.json"));
        await _repository.SaveInfoPokemonAttackInDB(json);
    }
}
