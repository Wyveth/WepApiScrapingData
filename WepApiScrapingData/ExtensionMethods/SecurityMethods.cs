using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using WebApiScrapingData.Infrastructure.Configurations;
using WebApiScrapingData.Infrastructure.Utils;
using WepApiScrapingData.Utils;

namespace WepApiScrapingData.ExtensionMethods
{
    public static class SecurityMethods
    {
        #region Constantes
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string AdministratorPolicy = "Administrator";
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Add Cors Configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomCors(configuration);
            services.AddCustomAuthentification(configuration);
            var adminUserIds = configuration.GetSection("Jwt:AdminUserIds").Get<string[]>() ?? [];
            services.AddAuthorization(options =>
                options.AddPolicy(AdministratorPolicy, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context =>
                    {
                        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                        return !string.IsNullOrWhiteSpace(userId) &&
                               adminUserIds.Any(id => !string.IsNullOrWhiteSpace(id) &&
                                   string.Equals(id, userId, StringComparison.Ordinal));
                    })));
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOption corsOption = new CorsOption();
            configuration.GetSection(Constantes.CorsOption).Bind(corsOption);

            services.AddCors(options => {
                options.AddPolicy(name: DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        public static void AddCustomAuthentification(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOption securityOption = new SecurityOption();
            configuration.GetSection(Constantes.SecurityOption).Bind(securityOption);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityOption.Key)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
            });

        }
        #endregion
    }
}
