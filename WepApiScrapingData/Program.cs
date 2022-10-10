using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.GraphQL;
using WepApiScrapingData.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ajout du DBContext
builder.Services.AddDbContextFactory<ScrapingContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PokemonDataBase"), sqlOptions => { });
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddInjections().AddCustomSecurity(builder.Configuration); ;

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();

var app = builder.Build();

string content = Path.Combine(app.Environment.ContentRootPath, "Content");
if (!Directory.Exists(content))
{
    Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "Content"));
}

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
    RequestPath = new PathString("/Content")
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(SecurityMethods.DEFAULT_POLICY);

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();
