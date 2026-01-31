using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(EnovaConnectionOptions opt)
        {

            return opt.AuthType switch
            {
                EnovaAuthType.None => Ok(new
                {
                    auth = "none"
                }),

                EnovaAuthType.Simple => Ok(new
                {
                    token = EnovaTokenStore.CreateToken(opt.Database),
                    type = "simple"
                }),

                EnovaAuthType.JWT => Ok(new
                {
                    token = new JwtTokenService().Generate(opt.Database),
                    type = "jwt"
                }),

                _ => BadRequest()
            };
        }
    }
}
