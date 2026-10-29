using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace WebApiScrapingData.Test.Security;

[TestFixture]
public class GraphQlSecurityTests
{
    [Test]
    public async Task DeeplyNestedGraphQlDocumentDoesNotTerminateTheApi()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        var start = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        start.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "WepApiScrapingData.dll"));
        start.Environment["ASPNETCORE_URLS"] = $"http://127.0.0.1:{port}";
        start.Environment["ASPNETCORE_ENVIRONMENT"] = "Production";
        start.Environment["ConnectionStrings__PokemonDataBase"] = "Server=localhost,1;Database=TestOnly;User Id=test;Password=test-only;Connect Timeout=1;TrustServerCertificate=True";
        start.Environment["Jwt__Key"] = "test-only-key-01234567890123456789012345678901234567890123456789";
        using var process = Process.Start(start) ?? throw new InvalidOperationException("Unable to start API test server.");
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
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

            string query = "{ __typename(arg:" + new string('[', 24000) + "0" + new string(']', 24000) + ") }";
            HttpResponseMessage response;
            try { response = await client.PostAsJsonAsync("/graphql", new { query }); }
            catch (HttpRequestException) { Assert.Fail("The API terminated while parsing a deeply nested GraphQL document."); throw; }
            using (response)
            {
                var body = JObject.Parse(await response.Content.ReadAsStringAsync());
                Assert.That(body["errors"], Is.Not.Null);
            }
            using var after = await client.GetAsync("/api/Health/status");
            Assert.That(after.StatusCode, Is.EqualTo(HttpStatusCode.OK));
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
}
