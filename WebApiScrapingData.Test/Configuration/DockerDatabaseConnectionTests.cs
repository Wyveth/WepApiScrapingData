using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace WebApiScrapingData.Test.Configuration;

[TestFixture]
public class DockerDatabaseConnectionTests
{
    [TestCase(true, true, true, true)]
    [TestCase(false, true, true, false)]
    [TestCase(true, false, true, false)]
    [TestCase(true, true, false, false)]
    public async Task ApiConnectsToTheAppropriateConfiguredServer(bool container, bool development, bool hasDockerConnection, bool expectDocker)
    {
        using var localServer = new TcpListener(IPAddress.Loopback, 0);
        using var dockerServer = new TcpListener(IPAddress.Loopback, 0);
        localServer.Start();
        dockerServer.Start();
        int localPort = ((IPEndPoint)localServer.LocalEndpoint).Port;
        int dockerPort = ((IPEndPoint)dockerServer.LocalEndpoint).Port;
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int apiPort = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        string testHome = Path.Combine(Path.GetTempPath(), "pokemon-config-test-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(testHome);

        var start = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        start.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "WepApiScrapingData.dll"));
        start.Environment["ASPNETCORE_URLS"] = $"http://127.0.0.1:{apiPort}";
        start.Environment["ASPNETCORE_ENVIRONMENT"] = development ? "Development" : "Production";
        start.Environment["DOTNET_RUNNING_IN_CONTAINER"] = container ? "true" : "false";
        start.Environment["APPDATA"] = testHome;
        start.Environment["HOME"] = testHome;
        start.Environment["Jwt__Key"] = "test-only-key-01234567890123456789012345678901234567890123456789";
        start.Environment["ConnectionStrings__PokemonDataBase"] = ConnectionString(localPort);
        start.Environment.Remove("ConnectionStrings__PokemonDataBaseDocker");
        if (hasDockerConnection)
            start.Environment["ConnectionStrings__PokemonDataBaseDocker"] = ConnectionString(dockerPort);

        using var process = Process.Start(start) ?? throw new InvalidOperationException("Unable to start API test server.");
        using var client = new HttpClient { BaseAddress = new Uri($"http://127.0.0.1:{apiPort}"), Timeout = TimeSpan.FromSeconds(10) };
        try
        {
            for (int attempt = 0; attempt < 100; attempt++)
            {
                if (process.HasExited) throw new InvalidOperationException("API test server exited prematurely.");
                try
                {
                    using var health = await client.GetAsync("/api/Health/status");
                    if (health.IsSuccessStatusCode) break;
                }
                catch (HttpRequestException) { }
                await Task.Delay(100);
            }

            var localAttempt = localServer.AcceptTcpClientAsync();
            var dockerAttempt = dockerServer.AcceptTcpClientAsync();
            var request = client.GetAsync("/api/v1.0/TypePok");
            var connected = await Task.WhenAny(localAttempt, dockerAttempt).WaitAsync(TimeSpan.FromSeconds(10));
            using var sqlConnection = await connected;
            Assert.That(connected == dockerAttempt, Is.EqualTo(expectDocker));
            sqlConnection.Close();
            using var response = await request;
        }
        finally
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
                process.WaitForExit();
            }
            Directory.Delete(testHome, recursive: true);
        }
    }

    private static string ConnectionString(int port) =>
        $"Server=127.0.0.1,{port};Database=test;User Id=test;Password=test-only;Encrypt=false;Connect Timeout=2;ConnectRetryCount=0";
}
