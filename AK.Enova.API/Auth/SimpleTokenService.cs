using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    public static class SimpleTokenService
    {
        public static string Generate()
            => Guid.NewGuid().ToString("N");
    }
}
