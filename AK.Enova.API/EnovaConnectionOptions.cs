using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public class EnovaConnectionOptions
    {
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public EnovaAuthType AuthType { get; set; } = EnovaAuthType.None;


    }
    public enum EnovaAuthType
    {
        None,
        JWT,
        Simple
    }

}
