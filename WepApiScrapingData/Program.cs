using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.GraphQL;
using WebApiScrapingData.Infrastructure.Loggers;
using WepApiScrapingData.Controllers;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(m => {
    m.SwaggerDoc("v1", new OpenApiInfo { Title = "PokeApi", Version = "v1" });

    m.OrderActionsBy(apiDesc =>
    {
        var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];     
        var httpMethod = apiDesc.HttpMethod;

        // Assignez des valeurs numériques plus grandes aux méthodes DELETE
        var httpMethodOrder = httpMethod.ToLower() == "delete" ? 4 :
                              httpMethod.ToLower() == "put" ? 3 :
                              httpMethod.ToLower() == "post" ? 2 :
                              httpMethod.ToLower() == "get" ? 1 : 0;

        var relativePath = apiDesc.RelativePath;

        // Concaténation des critères pour former une clé composite
        return $"{controllerName}_{httpMethodOrder}_{relativePath}";
    });
});

// 🔹 On charge la config du fichier JSON + les variables d'environnement
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//Ajout du DBContext
builder.Services.AddDbContextFactory<ScrapingContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PokemonDataBase");
    options.UseSqlServer(connectionString);
});

builder.Services.AddHttpClient("pokeapi", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    //options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ScrapingContext>();


builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddInjections()
                .AddCustomSecurity(builder.Configuration);

builder.Services.TryAddEnumerable(
    ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());
LoggerProviderOptions.RegisterProviderOptions<CustomLoggerConfiguration, CustomLoggerProvider>(builder.Services);

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 443;
    });
}

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    // Ne pas compresser les images
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes
        .Where(m => !m.StartsWith("image/"));
});

//builder.Services.Configure<JwtOptions>(
//    builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

app.UseResponseCompression();

app.UseMiddleware<LogRequestMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

string content = Path.Combine(app.Environment.ContentRootPath, "Content");
if (!Directory.Exists(content))
{
    Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "Content"));
}

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
    RequestPath = new PathString("/Content")
});

app.UseRouting();

app.UseCors(SecurityMethods.DEFAULT_POLICY);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGraphQL("/graphql");

app.Run();
