using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContextFactory : IDesignTimeDbContextFactory<ScrapingContext>
    {
        // Doit rester identique au <UserSecretsId> de WepApiScrapingData.csproj
        private const string ApiUserSecretsId = "4aebcedd-4c6a-49dc-ada7-472188070c3a";

        public ScrapingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScrapingContext>();
            string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PokemonDataBase");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            // Secrets utilisateur du projet API (poste de développement), comme au lancement de l'API
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                IConfigurationRoot secrets = new ConfigurationBuilder()
                    .AddUserSecrets(ApiUserSecretsId)
                    .Build();

                connectionString = secrets.GetConnectionString("PokemonDataBase");
            }

            // 1️⃣ Essayer de charger la configuration depuis appSettings.json
            try
            {
                string basePath = AppContext.BaseDirectory; // répertoire de sortie bin/Debug/net6.0 ou net7.0/...
                string appSettingsPath = System.IO.Path.Combine(basePath, "Settings", "appSettings.json");

                if (string.IsNullOrWhiteSpace(connectionString) && File.Exists(appSettingsPath))
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

            // 3️⃣ Si aucune connection string n'est trouvée, lancer une exception
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "La chaîne de connexion n'a pas été trouvée. " +
                    "Définissez le secret utilisateur 'ConnectionStrings:PokemonDataBase' du projet WepApiScrapingData, " +
                    "la variable d'environnement ConnectionStrings__PokemonDataBase (ou DB_CONNECTION), " +
                    "ou un fichier Settings/appSettings.json."
                );
            }

            // 4️⃣ Configurer le DbContext avec SQL Server
            optionsBuilder.UseSqlServer(connectionString);

            return new ScrapingContext(optionsBuilder.Options);
        }
    }
}
