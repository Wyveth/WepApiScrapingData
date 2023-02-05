using WebApiScrapingData.Domain.Class;
using WebApiScrapingData.Infrastructure.Data;

namespace WebApiScrapingData.Infrastructure.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TypePok> GetTypesPok([Service] ScrapingContext context) =>
            context.TypesPok;

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Pokemon> GetPokemons([Service] ScrapingContext context) =>
            context.Pokemons;
    }
}
