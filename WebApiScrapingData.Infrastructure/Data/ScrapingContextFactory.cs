using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContextFactory : IDesignTimeDbContextFactory<ScrapingContext>
    {
        public ScrapingContext CreateDbContext(string[] args)
        {
            // 1️⃣ Obtenir le répertoire de la solution, même depuis bin/Debug/net6.0
            string basePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));

            // 2️⃣ Construire le chemin complet vers appSettings.json
            string appSettingsPath = System.IO.Path.Combine(basePath, "WebApiScrapingData.Infrastructure", "Settings", "appSettings.json");

            // 3️⃣ Vérifier que le fichier existe
            if (!File.Exists(appSettingsPath))
            {
                throw new FileNotFoundException($"Le fichier de configuration '{appSettingsPath}' n'a pas été trouvé. " +
                    "Vérifiez qu'il est bien présent et qu'il est configuré pour être copié dans le répertoire de sortie si nécessaire.");
            }

            // 4️⃣ Construire la configuration à partir de appSettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetDirectoryName(appSettingsPath)) // répertoire contenant le fichier JSON
                .AddJsonFile(System.IO.Path.GetFileName(appSettingsPath), optional: false, reloadOnChange: true)
                .Build();

            // 5️⃣ Créer DbContextOptions avec la chaîne de connexion
            var optionsBuilder = new DbContextOptionsBuilder<ScrapingContext>();
            string connectionString = configuration.GetConnectionString("PokemonDataBase");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion 'PokemonDataBase' n'a pas été trouvée dans appSettings.json.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            // 6️⃣ Retourner le DbContext
            return new ScrapingContext(optionsBuilder.Options);
        }
    }
}
