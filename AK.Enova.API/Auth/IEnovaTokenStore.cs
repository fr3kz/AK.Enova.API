using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API.Auth
{
    public interface IEnovaTokenStore
    {
        EnovaAccessToken Create(string database);
        EnovaAccessToken? Validate(string token);
        void Revoke(string token);
    }
}
