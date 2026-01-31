using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    public sealed class EnovaJwtMiddleware
    {
        private readonly RequestDelegate _next;

        public EnovaJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IOptions<EnovaConnectionOptions> options)
        {
            var opt = options.Value;

            if (opt.AuthType == EnovaAuthType.None)
            {
                await _next(context);
                return;
            }

            if (context.Request.Path.StartsWithSegments("/api/auth"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var auth))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var header = auth.ToString();

            if (!header.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var token = header.Substring("Bearer ".Length);

            var principal = JwtTokenService.Validate(token);

            if (principal == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }


}
