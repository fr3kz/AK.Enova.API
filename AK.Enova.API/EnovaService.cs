using Soneta.Business;
using Soneta.Business.App;
using Soneta.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public class EnovaService
    {
        public Session Session { get; set; }

        public EnovaService(EnovaConnectionOptions options)
        {
            SessionState.ResetAttaching();
            SessionState.Create().Attach();

            var db = BusApplication.Instance[options.Database];
            var login = db.Login(false, options.User, options.Password);

            Session = login.CreateSession(false, true,"API");
        }


    }

}
