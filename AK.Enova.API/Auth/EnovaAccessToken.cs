using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    public class EnovaAccessToken
    {
        public string Token { get; set; } = Guid.NewGuid().ToString("N");
        public string Database { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
