using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    using Microsoft.Extensions.Options;

    public class EnovaAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EnovaConnectionOptions _settings;

        public EnovaAuthMiddleware(
            RequestDelegate next,
            IOptions<EnovaConnectionOptions> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext ctx)
        {
            if (_settings.AuthType == EnovaAuthType.None)
            {
                await _next(ctx);
                return;
            }

            var token = ctx.Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                ctx.Response.StatusCode = 401;
                await ctx.Response.WriteAsync("Missing token");
                return;
            }

            bool valid =
                _settings.AuthType == EnovaAuthType.Simple
                    ? EnovaTokenStore.Validate(token)
                    : JwtTokenService.Validate(token);

            if (!valid)
            {
                ctx.Response.StatusCode = 401;
                await ctx.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(ctx);
        }
    }


}
