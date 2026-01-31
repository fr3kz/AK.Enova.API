using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SymmetricSecurityKey = System.IdentityModel.Tokens.SymmetricSecurityKey;

namespace AK.Enova.API.Auth
{
    public static class JwtTokenService
    {
        private static readonly string Secret =
        "AK_ENOVA_SUPER_SECRET_2026_CHANGE_THIS_TO_64_CHARS_MINIMUM";

        public static string Generate(string database, string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(Secret);

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("db", database),
                new Claim("access", accessToken)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            });

            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal? Validate(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var principal = handler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                               new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Secret)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    },
                    out _);

                return principal;
            }
            catch
            {
                return null;
            }
        
         }
}
}
