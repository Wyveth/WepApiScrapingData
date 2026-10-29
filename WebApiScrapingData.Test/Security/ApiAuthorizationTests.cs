using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApiScrapingData.Test.Security;

[TestFixture(true, false)]
[TestFixture(false, false)]
[TestFixture(true, true)]
public class ApiAuthorizationTests
{
    private const string TestKey = "test-only-key-01234567890123456789012345678901234567890123456789";
    private Process _server = null!;
    private HttpClient _client = null!;
    private readonly bool _configureAdministrator;
    private readonly bool _useDevelopmentSecrets;
    private string? _testDirectory;

    public ApiAuthorizationTests(bool configureAdministrator, bool useDevelopmentSecrets)
    {
        _configureAdministrator = configureAdministrator;
        _useDevelopmentSecrets = useDevelopmentSecrets;
    }

    [OneTimeSetUp]
    public async Task StartServer()
    {
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
        start.Environment["ASPNETCORE_ENVIRONMENT"] = _useDevelopmentSecrets ? "Development" : "Production";
        start.Environment["ConnectionStrings__PokemonDataBase"] = "Server=localhost,1;Database=TestOnly;User Id=test;Password=test-only;Connect Timeout=1;TrustServerCertificate=True";
        if (_useDevelopmentSecrets)
        {
            _testDirectory = Path.Combine(Path.GetTempPath(), "pokemon-api-test-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_testDirectory);
            start.WorkingDirectory = _testDirectory;
            File.WriteAllText(Path.Combine(_testDirectory, "appsettings.json"), "{\"Jwt\":{\"Key\":\"different-file-key-0123456789012345678901234567890123456789012345\"}}");
            var secretsRoot = OperatingSystem.IsWindows()
                ? Path.Combine(_testDirectory, "Microsoft", "UserSecrets")
                : Path.Combine(_testDirectory, ".microsoft", "usersecrets");
            var secretsDirectory = Path.Combine(secretsRoot, "4aebcedd-4c6a-49dc-ada7-472188070c3a");
            Directory.CreateDirectory(secretsDirectory);
            File.WriteAllText(Path.Combine(secretsDirectory, "secrets.json"), $"{{\"Jwt:Key\":\"{TestKey}\"}}");
            if (OperatingSystem.IsWindows())
                start.Environment["APPDATA"] = _testDirectory;
            else
                start.Environment.Remove("APPDATA");
            start.Environment["HOME"] = _testDirectory;
            start.Environment.Remove("Jwt__Key");
        }
        else
        {
            start.Environment["Jwt__Key"] = TestKey;
        }
        foreach (var key in start.Environment.Keys.Where(k => k.StartsWith("Jwt__AdminUserIds__", StringComparison.OrdinalIgnoreCase)).ToArray())
            start.Environment.Remove(key);
        if (_configureAdministrator)
            start.Environment["Jwt__AdminUserIds__0"] = "admin-test";
        _server = Process.Start(start) ?? throw new InvalidOperationException("Unable to start API test server.");
        _client = new HttpClient { BaseAddress = new Uri($"http://127.0.0.1:{port}"), Timeout = TimeSpan.FromSeconds(3) };

        for (int attempt = 0; attempt < 100; attempt++)
        {
            if (_server.HasExited)
                throw new InvalidOperationException($"API test server exited with code {_server.ExitCode}.");
            try
            {
                using var response = await _client.GetAsync("/api/Health/status");
                if (response.IsSuccessStatusCode) return;
            }
            catch (HttpRequestException) { }
            await Task.Delay(100);
        }

        throw new TimeoutException("API test server did not start.");
    }

    [OneTimeTearDown]
    public void StopServer()
    {
        _client?.Dispose();
        if (_server is { HasExited: false })
        {
            _server.Kill(entireProcessTree: true);
            _server.WaitForExit();
        }
        _server?.Dispose();
        if (_testDirectory != null && Directory.Exists(_testDirectory))
            Directory.Delete(_testDirectory, recursive: true);
    }

    [Test]
    public async Task AnonymousUserCannotAccessDiagnosticEndpoint()
    {
        using var response = await _client.GetAsync("/api/Debug/scanfiles");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task AnonymousUserCannotWritePokemon()
    {
        using var response = await _client.PostAsync("/api/v1.0/Pokemon/Add", new StringContent("{invalid-json", Encoding.UTF8, "application/json"));
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task OrdinaryUserCannotAccessDiagnosticEndpoint()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/Debug/scanfiles");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CreateToken("ordinary-user"));
        using var response = await _client.SendAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [Test]
    public async Task OnlyConfiguredAdministratorCanAccessDiagnosticEndpoint()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/Debug/scanfiles");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CreateToken("admin-test"));
        using var response = await _client.SendAsync(request);
        if (_configureAdministrator)
            Assert.That(response.StatusCode, Is.AnyOf(HttpStatusCode.OK, HttpStatusCode.NotFound));
        else
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
    }

    [TestCase("/api/v1.0/Database/ImportDb")]
    [TestCase("/api/v1.0/SynchroPoke/SynchroAttacksFromPokeApi")]
    [TestCase("/api/v1.0/Scraping/ScrapingAll")]
    [TestCase("/api/v1.0/Xamarin/Generate/JsonPokemonV1")]
    [TestCase("/api/v1.0/Quizz/GenerateQuizz")]
    [TestCase("/api/v1.0/Question/GenerateAnswers/1/1")]
    public async Task AdministrativeGetRequiresAuthentication(string path)
    {
        using var response = await _client.GetAsync(path);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [TestCase("register")]
    [TestCase("login")]
    [TestCase("refresh")]
    public async Task AuthenticationEndpointsRemainPublic(string action)
    {
        using var response = await _client.PostAsync($"/api/v1.0/Authenticate/{action}", new StringContent("{invalid-json", Encoding.UTF8, "application/json"));
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task HealthEndpointRemainsPublic()
    {
        using var response = await _client.GetAsync("/api/Health/status");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task PokemonReadRemainsPublic()
    {
        using var response = await _client.GetAsync("/api/v1.0/Pokemon/GetByGuid/not-a-guid");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GenerationImportRemainsProtectedAndResolvesItsDependencies()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1.0/Database/SaveGenInDB/2147483647");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CreateToken("admin-test"));
        using var response = await _client.SendAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(_configureAdministrator
            ? HttpStatusCode.InternalServerError // File absent: the import service was reached.
            : HttpStatusCode.Forbidden));
        if (_configureAdministrator && _useDevelopmentSecrets)
            Assert.That(await response.Content.ReadAsStringAsync(), Does.Contain("PokeScrapGen2147483647.json"));
    }

    private static string CreateToken(string userId)
    {
        var token = new JwtSecurityToken(
            claims: [new Claim(ClaimTypes.NameIdentifier, userId)],
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TestKey)), SecurityAlgorithms.HmacSha512));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
