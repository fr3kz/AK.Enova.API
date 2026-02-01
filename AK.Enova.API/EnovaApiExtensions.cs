using AK.Enova.API.Auth;
using AK.Enova.API.Auth.AK.Enova.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace AK.Enova.API
{
    public static class EnovaApiExtensions
    {
        public static IServiceCollection AddEnovaApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddControllers()
                .AddApplicationPart(typeof(EnovaService).Assembly);

            services.Configure<EnovaConnectionOptions>(options =>
            {
                options.Database = configuration["Enova:Database"];
                options.User = configuration["Enova:User"];
                options.Password = configuration["Enova:Password"];

                var authType = configuration["Enova:AuthType"];

                options.AuthType =
                    string.Equals(authType, "JWT", StringComparison.OrdinalIgnoreCase)
                        ? EnovaAuthType.JWT
                        : EnovaAuthType.None;
            });

            services.AddSingleton<IEnovaTokenStore>(
                new SqliteTokenStore("enova_tokens.db"));

            services.AddScoped<EnovaSessionFilter>();

            services.AddSingleton<EnovaService>(sp =>
            {
                var opt = sp
                    .GetRequiredService<IOptions<EnovaConnectionOptions>>()
                    .Value;

                return new EnovaService(opt);
            });

            return services;
        }

        public static WebApplication UseEnovaApi(
            this WebApplication app)
        {
            app.UseEnovaAuth();
            app.MapEnovaAuthEndpoints();
            app.MapControllers();
            return app;
        }

        public static IApplicationBuilder UseEnovaAuth(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<EnovaJwtMiddleware>();
        }
    }
}
