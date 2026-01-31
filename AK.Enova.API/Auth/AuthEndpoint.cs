using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace AK.Enova.API.Auth
{
    public static class AuthEndpoints
    {
        public static void MapEnovaAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login",
            (
                LoginRequest req,
                IEnovaTokenStore tokenStore,
                IOptions<EnovaConnectionOptions> options
            ) =>
            {
                var opt = options.Value;

                if (opt.AuthType == EnovaAuthType.None)
                    return Results.Ok(new { auth = "none" });

                if (string.IsNullOrWhiteSpace(req.AccessToken))
                    return Results.BadRequest("Access token required");

                var access = tokenStore.Validate(req.AccessToken);

                if (access == null)
                    return Results.Unauthorized();

                if (opt.AuthType == EnovaAuthType.JWT)
                {
                    var jwt = JwtTokenService.Generate(
                        opt.Database,
                        access.Token);

                    return Results.Ok(new
                    {
                        token = jwt,
                        type = "jwt",
                        expires = 1800
                    });
                }

                return Results.BadRequest();
            });
        }

    }
}
