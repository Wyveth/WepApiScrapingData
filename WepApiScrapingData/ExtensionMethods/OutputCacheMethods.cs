using Microsoft.AspNetCore.OutputCaching;

namespace WepApiScrapingData.ExtensionMethods
{
    public static class OutputCacheMethods
    {
        #region Constantes
        public const string READ_DATA_POLICY = "ReadData";
        public const string READ_DATA_TAG = "read-data";
        #endregion

        #region Public Methods
        /// <summary>
        /// Cache serveur des lectures de données (Pokémon) : quasi statiques, coûteuses à charger.
        /// Les requêtes authentifiées (en-tête Authorization) ne sont pas mises en cache (politique par défaut).
        /// </summary>
        public static IServiceCollection AddCustomOutputCache(this IServiceCollection services)
        {
            services.AddOutputCache(options =>
            {
                options.AddPolicy(READ_DATA_POLICY, policy => policy
                    .Expire(TimeSpan.FromMinutes(10))
                    .Tag(READ_DATA_TAG));
            });
            return services;
        }

        /// <summary>
        /// Vide le cache des lectures après toute écriture réussie (import, synchro, CRUD),
        /// pour ne jamais servir de données périmées plus longtemps que la requête d'écriture.
        /// </summary>
        public static IApplicationBuilder UseReadDataCacheEviction(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                await next();

                var method = context.Request.Method;
                bool isWrite = !(HttpMethods.IsGet(method) || HttpMethods.IsHead(method) || HttpMethods.IsOptions(method));
                bool isGraphQl = context.Request.Path.StartsWithSegments("/graphql");

                if (isWrite && !isGraphQl && context.Response.StatusCode < 400)
                {
                    var store = context.RequestServices.GetRequiredService<IOutputCacheStore>();
                    await store.EvictByTagAsync(READ_DATA_TAG, CancellationToken.None);
                }
            });
        }
        #endregion
    }
}
