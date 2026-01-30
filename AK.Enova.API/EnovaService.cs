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
        public Session Session { get; }

        public EnovaService(string database)
        {
            SessionState.ResetAttaching();
            SessionState.Create().Attach();

            var db = BusApplication.Instance[database];
            var login = db.Login(false, "Administrator", "");

            Session = login.CreateSession(false, true);
        }
    }

}
