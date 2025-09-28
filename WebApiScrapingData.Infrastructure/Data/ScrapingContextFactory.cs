using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContextFactory : IDesignTimeDbContextFactory<ScrapingContext>
    {
        public ScrapingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScrapingContext>();
            string connectionString = null;

            // 1️⃣ Essayer de charger la configuration depuis appSettings.json
            try
            {
                string basePath = AppContext.BaseDirectory; // répertoire de sortie bin/Debug/net6.0 ou net7.0/...
                string appSettingsPath = System.IO.Path.Combine(basePath, "Settings", "appSettings.json");

                if (File.Exists(appSettingsPath))
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(System.IO.Path.GetDirectoryName(appSettingsPath))
                        .AddJsonFile(System.IO.Path.GetFileName(appSettingsPath), optional: false, reloadOnChange: true)
                        .Build();

                    connectionString = configuration.GetConnectionString("PokemonDataBase");
                }
            }
            catch
            {
                // Ignorer et passer au fallback
            }

            // 2️⃣ Fallback sur variable d'environnement si nécessaire
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            }

            // 3️⃣ Si aucune connection string n'est trouvée, lancer une exception
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "La chaîne de connexion n'a pas été trouvée. " +
                    "Vérifiez que 'appSettings.json' est présent dans Settings ou que la variable d'environnement DB_CONNECTION est définie."
                );
            }

            // 4️⃣ Configurer le DbContext avec SQL Server
            optionsBuilder.UseSqlServer(connectionString);

            return new ScrapingContext(optionsBuilder.Options);
        }
    }
}
