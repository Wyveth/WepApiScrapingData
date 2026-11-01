using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Test.Configuration;

[TestFixture]
[NonParallelizable]
public class ScrapingContextFactoryTests
{
    [Test]
    public void EnvironmentConnectionOverridesFileForDesignTimeContext()
    {
        const string expected = "Server=localhost;Database=TestOnly;User Id=test;Password=test-only;TrustServerCertificate=True";
        var previous = Environment.GetEnvironmentVariable("DB_CONNECTION");
        var standard = Environment.GetEnvironmentVariable("ConnectionStrings__PokemonDataBase");
        try
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__PokemonDataBase", null);
            Environment.SetEnvironmentVariable("DB_CONNECTION", expected);
            using var context = new ScrapingContextFactory().CreateDbContext([]);
            var builder = new SqlConnectionStringBuilder(context.Database.GetConnectionString());
            Assert.That(builder.DataSource, Is.EqualTo("localhost"));
            Assert.That(builder.InitialCatalog, Is.EqualTo("TestOnly"));
            Assert.That(builder.UserID, Is.EqualTo("test"));
            Assert.That(builder.Password, Is.EqualTo("test-only"));
            Assert.That(builder.TrustServerCertificate, Is.True);
        }
        finally
        {
            Environment.SetEnvironmentVariable("DB_CONNECTION", previous);
            Environment.SetEnvironmentVariable("ConnectionStrings__PokemonDataBase", standard);
        }
    }

    [Test]
    public void EmptyStandardConnectionDoesNotHideLegacyEnvironmentConnection()
    {
        const string expected = "Server=localhost;Database=TestOnly;User Id=test;Password=test-only;TrustServerCertificate=True";
        var standard = Environment.GetEnvironmentVariable("ConnectionStrings__PokemonDataBase");
        var legacy = Environment.GetEnvironmentVariable("DB_CONNECTION");
        try
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__PokemonDataBase", " ");
            Environment.SetEnvironmentVariable("DB_CONNECTION", expected);
            using var context = new ScrapingContextFactory().CreateDbContext([]);
            var builder = new SqlConnectionStringBuilder(context.Database.GetConnectionString());
            Assert.That(builder.DataSource, Is.EqualTo("localhost"));
            Assert.That(builder.InitialCatalog, Is.EqualTo("TestOnly"));
            Assert.That(builder.UserID, Is.EqualTo("test"));
            Assert.That(builder.Password, Is.EqualTo("test-only"));
            Assert.That(builder.TrustServerCertificate, Is.True);
        }
        finally
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__PokemonDataBase", standard);
            Environment.SetEnvironmentVariable("DB_CONNECTION", legacy);
        }
    }
}
