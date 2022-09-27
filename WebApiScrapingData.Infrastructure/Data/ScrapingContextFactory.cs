using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApiScrapingData.Infrastructure.Data
{
    public class ScrapingContextFactory : IDesignTimeDbContextFactory<ScrapingContext>
    {
        public ScrapingContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            string path = Directory.GetCurrentDirectory() + "x";
            path = path.Replace(Directory.GetCurrentDirectory().Split('\\')[Directory.GetCurrentDirectory().Split('\\').Length - 1] + "x", "WebApiScrapingData.Infrastructure");

            configurationBuilder.AddJsonFile(System.IO.Path.Combine(path, "Settings", "appSettings.json"));

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(configurationRoot.GetConnectionString("PokemonDataBase"));//Code Pour récupéré l'assembly!!!

            ScrapingContext context = new ScrapingContext(builder.Options);
            return context;
        }
    }
}
