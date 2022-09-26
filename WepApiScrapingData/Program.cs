using Microsoft.EntityFrameworkCore;
using WebApiScrapingData.Infrastructure.Data;
using WepApiScrapingData.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Ajout du DBContext
builder.Services.AddDbContext<ScrapingContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PokemonDataBase"), sqlOptions => { });
});

builder.Services.AddInjections();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
