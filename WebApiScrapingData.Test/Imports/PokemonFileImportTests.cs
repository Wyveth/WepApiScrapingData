using WebApiScrapingData.Core.Repositories;
using WepApiScrapingData.Services;

namespace WebApiScrapingData.Test.Imports;

[TestFixture]
public class PokemonFileImportTests
{
    private string _root = null!;

    [SetUp]
    public void SetUp()
    {
        _root = Path.Combine(Path.GetTempPath(), "pokemon-import-test-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(_root, "PokeScrap"));
    }

    [TearDown]
    public void TearDown() => Directory.Delete(_root, recursive: true);

    [TestCase(null, "PokeScrap.json")]
    [TestCase(1, "PokeScrapGen1.json")]
    [TestCase(9, "PokeScrapGen9.json")]
    public async Task PokemonImportReadsExpectedFileAndAwaitsRepository(int? generation, string filename)
    {
        await File.WriteAllTextAsync(Path.Combine(_root, "PokeScrap", filename), "[{\"Number\":\"001\"}]");
        var (service, importer) = CreateService();

        var task = service.ImportPokemonAsync(generation);
        await importer.Called.Task.WaitAsync(TimeSpan.FromSeconds(5));

        Assert.That(importer.MethodName, Is.EqualTo("SaveJsonInDb"));
        Assert.That(importer.Json, Is.EqualTo("[{\"Number\":\"001\"}]"));
        Assert.That(task.IsCompleted, Is.False);
        importer.Completion.SetResult();
        await task;
    }

    [Test]
    public async Task AttackImportReadsExpectedFileAndPropagatesFailure()
    {
        await File.WriteAllTextAsync(Path.Combine(_root, "PokeScrap", "PokeBipScrapGen.json"), "[{\"Name\":\"Test\"}]");
        var (service, importer) = CreateService();
        var task = service.ImportAttackInfoAsync();
        await importer.Called.Task.WaitAsync(TimeSpan.FromSeconds(5));
        Assert.That(importer.MethodName, Is.EqualTo("SaveInfoPokemonAttackInDB"));
        Assert.That(importer.Json, Is.EqualTo("[{\"Name\":\"Test\"}]"));
        importer.Completion.SetException(new InvalidOperationException("import failed"));
        var error = Assert.ThrowsAsync<InvalidOperationException>(async () => await task);
        Assert.That(error!.Message, Is.EqualTo("import failed"));
    }

    [Test]
    public void MissingFileDoesNotInvokeRepository()
    {
        var (service, importer) = CreateService();
        Assert.ThrowsAsync<FileNotFoundException>(async () => await service.ImportPokemonAsync(3));
        Assert.That(importer.Called.Task.IsCompleted, Is.False);
    }

    private (PokemonFileImportService service, FakeImporter importer) CreateService()
    {
        var importer = new FakeImporter();
        return (new PokemonFileImportService(_root, importer), importer);
    }

    private sealed class FakeImporter : IPokemonFileImportRepository
    {
        public TaskCompletionSource Called { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public TaskCompletionSource Completion { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public string? MethodName { get; private set; }
        public string? Json { get; private set; }

        public Task SaveJsonInDb(string json) => Record(nameof(SaveJsonInDb), json);
        public Task SaveInfoPokemonAttackInDB(string json) => Record(nameof(SaveInfoPokemonAttackInDB), json);

        private Task Record(string methodName, string json)
        {
            MethodName = methodName;
            Json = json;
            Called.TrySetResult();
            return Completion.Task;
        }
    }
}
