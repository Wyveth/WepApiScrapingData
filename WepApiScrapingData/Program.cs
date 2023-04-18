using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;
using System.Net;
using WebApiScrapingData.Infrastructure.Data;
using WebApiScrapingData.Infrastructure.GraphQL;
using WebApiScrapingData.Infrastructure.Loggers;
using WepApiScrapingData.ExtensionMethods;
using WepApiScrapingData.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ajout du DBContext
builder.Services.AddDbContextFactory<ScrapingContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PokemonDataBase"));
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

var app = builder.Build();

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
