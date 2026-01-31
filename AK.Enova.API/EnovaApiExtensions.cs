using AK.Enova.API.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public static class EnovaApiExtensions
    {
        public static IServiceCollection AddEnovaApi(
            this IServiceCollection services)
        {
            services.AddSingleton<EnovaService>();
            return services;
        }

        public static WebApplication UseEnovaApi(
            this WebApplication app)
        {
            app.MapEnovaAuthEndpoints();
            return app;
        }
        public static IApplicationBuilder UseEnovaAuth(
       this IApplicationBuilder app)
        {
            return app.UseMiddleware<EnovaJwtMiddleware>();
        }
    }
}
