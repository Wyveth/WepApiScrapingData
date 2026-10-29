using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Domain.ClassJson;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.Repository.Class;

namespace WebApiScrapingData.Test.Imports;

[TestFixture]
[NonParallelizable]
public class PokemonImportSqlTests
{
    private const string TestPassword = "TestOnly!Pokemon2026";
    private string _connectionString = null!;

    [SetUp]
    public async Task SetUp()
    {
        var portText = Environment.GetEnvironmentVariable("POKEMON_IMPORT_TEST_SQL_PORT");
        if (string.IsNullOrWhiteSpace(portText))
            Assert.Ignore("Set POKEMON_IMPORT_TEST_SQL_PORT to the dedicated disposable SQL Server test container port.");
        if (!int.TryParse(portText, out var port) || port is < 1 or > 65535)
            throw new InvalidOperationException("Invalid isolated SQL Server test port.");

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = $"127.0.0.1,{port}",
            InitialCatalog = "master",
            UserID = "sa",
            Password = TestPassword,
            TrustServerCertificate = true,
            ConnectTimeout = 5
        };
        await using (var connection = new SqlConnection(builder.ConnectionString))
        {
            await connection.OpenAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT CAST(SERVERPROPERTY('MachineName') AS nvarchar(128))";
            var hostname = (string?)await command.ExecuteScalarAsync();
            if (!string.Equals(hostname, "poke-import-it", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Refusing to run SQL integration tests outside the dedicated test container.");
        }

        builder.InitialCatalog = "PokemonImportTest_" + Guid.NewGuid().ToString("N");
        _connectionString = builder.ConnectionString;
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }

    [Test]
    public async Task ReimportKeepsExistingPokemonAndLanguageRows()
    {
        await ImportAsync([Pokemon("006", "Dracaufeu", "Normal", 10)]);
        await ImportAsync([Pokemon("6", "dracaufeu", "normal", 99)]);

        await using var context = CreateContext();
        var pokemon = await context.Pokemons.SingleAsync();
        Assert.That(pokemon.StatPv, Is.EqualTo(10));
        Assert.That(await context.DataInfos.CountAsync(), Is.EqualTo(9));
        Assert.That(await context.EvolutionChain.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task RealGenerationFileCanBeReimportedWithoutDuplicates()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "PokeScrapGen1.json");
        var json = await File.ReadAllTextAsync(path);
        await using (var context = CreateContext())
            await new PokemonRepository(context).SaveJsonInDb(json);
        await using (var context = CreateContext())
            await new PokemonRepository(context).SaveJsonInDb(json);

        await using var verify = CreateContext();
        Assert.That(await verify.Pokemons.CountAsync(), Is.EqualTo(214));
        Assert.That(await verify.DataInfos.CountAsync(), Is.EqualTo(214 * 9));
    }

    [Test]
    public async Task DistinctFormsWithSameNumberAndTypeRemainSeparate()
    {
        await ImportAsync([
            Pokemon("006", "Méga-Dracaufeu X", "Mega", 10),
            Pokemon("006", "Méga-Dracaufeu Y", "Mega", 20)
        ]);

        await using var context = CreateContext();
        Assert.That(await context.Pokemons.CountAsync(), Is.EqualTo(2));
        Assert.That(await context.DataInfos.CountAsync(), Is.EqualTo(18));
    }

    [Test]
    public async Task GraphQlFiltersAndProjectsTypesFromSqlServer()
    {
        long id;
        await using (var context = CreateContext())
        {
            var type = new TypePok { Name_FR = "Test type" };
            context.TypesPok.Add(type);
            await context.SaveChangesAsync();
            id = type.Id;
        }

        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        var start = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        start.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "WepApiScrapingData.dll"));
        start.Environment["ASPNETCORE_URLS"] = $"http://127.0.0.1:{port}";
        start.Environment["ASPNETCORE_ENVIRONMENT"] = "Production";
        start.Environment["ConnectionStrings__PokemonDataBase"] = _connectionString;
        start.Environment["Jwt__Key"] = "test-only-key-01234567890123456789012345678901234567890123456789";

        using var process = Process.Start(start) ?? throw new InvalidOperationException("Unable to start API test server.");
        using var client = new HttpClient { BaseAddress = new Uri($"http://127.0.0.1:{port}"), Timeout = TimeSpan.FromSeconds(15) };
        try
        {
            bool ready = false;
            for (int attempt = 0; attempt < 100; attempt++)
            {
                if (process.HasExited) throw new InvalidOperationException("API test server exited prematurely.");
                try
                {
                    using var health = await client.GetAsync("/api/Health/status");
                    if (health.IsSuccessStatusCode) { ready = true; break; }
                }
                catch (HttpRequestException) { }
                await Task.Delay(100);
            }
            if (!ready) throw new TimeoutException("API test server did not start.");

            using var response = await client.PostAsJsonAsync("/graphql", new { query = $"{{ typesPok(where: {{ id: {{ eq: {id} }} }}) {{ id }} }}" });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var body = JObject.Parse(await response.Content.ReadAsStringAsync());
            Assert.That(body["errors"], Is.Null, body.ToString());
            Assert.That(body["data"]!["typesPok"]!.Select(item => item["id"]!.Value<long>()), Is.EqualTo(new[] { id }));
        }
        finally
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
                process.WaitForExit();
            }
        }
    }

    [Test]
    public async Task DuplicateWithinInputKeepsFirstEntry()
    {
        await ImportAsync([
            Pokemon("006", "Dracaufeu", "Normal", 10),
            Pokemon("6", " DRACAUFEU ", " normal ", 99)
        ]);

        await using var context = CreateContext();
        Assert.That((await context.Pokemons.SingleAsync()).StatPv, Is.EqualTo(10));
        Assert.That(await context.DataInfos.CountAsync(), Is.EqualTo(9));
    }

    [Test]
    public async Task InvalidEntryDoesNotLeavePartialImport()
    {
        var valid = Pokemon("006", "Dracaufeu", "Normal", 10);
        var invalid = Pokemon("007", "", "Normal", 20);
        Assert.ThrowsAsync<ArgumentException>(async () => await ImportAsync([valid, invalid]));

        await using var context = CreateContext();
        Assert.That(await context.Pokemons.CountAsync(), Is.Zero);
        Assert.That(await context.DataInfos.CountAsync(), Is.Zero);
        Assert.That(await context.EvolutionChain.CountAsync(), Is.Zero);
    }

    [Test]
    public async Task ExistingPokemonWithoutIdentityStopsImportWithoutChangingData()
    {
        await using (var seed = CreateContext())
        {
            var existing = new Pokemon { Number = "006", TypeEvolution = null, StatPv = 25 };
            existing.FR.Name = "Dracaufeu";
            seed.Pokemons.Add(existing);
            await seed.SaveChangesAsync();
        }

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await ImportAsync([Pokemon("007", "Carapuce", "Normal", 20)]));

        await using var context = CreateContext();
        Assert.That((await context.Pokemons.SingleAsync()).StatPv, Is.EqualTo(25));
        Assert.That(await context.DataInfos.CountAsync(), Is.EqualTo(9));
    }

    [Test]
    public async Task FailureAfterFirstPokemonRollsBackAllRows()
    {
        await using var failingContext = CreateContext(new FailSecondPokemonSave());
        var json = JsonConvert.SerializeObject(new[]
        {
            Pokemon("006", "Dracaufeu", "Normal", 10),
            Pokemon("007", "Carapuce", "Normal", 20)
        });
        Assert.ThrowsAsync<InvalidOperationException>(async () => await new PokemonRepository(failingContext).SaveJsonInDb(json));

        await using var context = CreateContext();
        Assert.That(await context.Pokemons.CountAsync(), Is.Zero);
        Assert.That(await context.DataInfos.CountAsync(), Is.Zero);
        Assert.That(await context.EvolutionChain.CountAsync(), Is.Zero);
    }

    [Test]
    public async Task ConcurrentImportsDoNotCreateDuplicates()
    {
        var json = JsonConvert.SerializeObject(new[] { Pokemon("006", "Dracaufeu", "Normal", 10) });
        async Task RunImport()
        {
            await using var context = CreateContext();
            await new PokemonRepository(context).SaveJsonInDb(json);
        }

        var attempts = new[] { RunImport(), RunImport() };
        async Task<Exception?> CaptureFailure(Task attempt)
        {
            try { await attempt; return null; }
            catch (Exception ex) { return ex; }
        }
        var failures = await Task.WhenAll(attempts.Select(CaptureFailure));
        foreach (var failure in failures.Where(failure => failure != null))
        {
            if (!IsDeadlock(failure!))
                throw failure!;
            await RunImport();
        }

        await using var context = CreateContext();
        Assert.That(await context.Pokemons.CountAsync(), Is.EqualTo(1));
        Assert.That(await context.DataInfos.CountAsync(), Is.EqualTo(9));
        Assert.That(await context.EvolutionChain.CountAsync(), Is.EqualTo(1));
    }

    private async Task ImportAsync(PokemonJson[] values)
    {
        await using var context = CreateContext();
        await new PokemonRepository(context).SaveJsonInDb(JsonConvert.SerializeObject(values));
    }

    private ScrapingContext CreateContext(SaveChangesInterceptor? interceptor = null)
    {
        var options = new DbContextOptionsBuilder<ScrapingContext>().UseSqlServer(_connectionString);
        if (interceptor != null) options.AddInterceptors(interceptor);
        return new ScrapingContext(options.Options);
    }

    private static PokemonJson Pokemon(string number, string name, string form, int hp) => new()
    {
        Number = number,
        TypeEvolution = form,
        StatPv = hp,
        FR = new DataInfoJson { Name = name, DisplayName = "Pokémon" }
    };

    private static bool IsDeadlock(Exception exception)
    {
        for (Exception? current = exception; current != null; current = current.InnerException)
            if (current is SqlException { Number: 1205 }) return true;
        return false;
    }

    private sealed class FailSecondPokemonSave : SaveChangesInterceptor
    {
        private int _pokemonSaves;
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context!.ChangeTracker.Entries<Pokemon>().Any(e => e.State == EntityState.Added) && ++_pokemonSaves == 2)
                throw new InvalidOperationException("Simulated SQL save failure.");
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
