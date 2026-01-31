using Microsoft.AspNetCore.Mvc;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kadry;
using Soneta.SrodkiTrwale;
using System;

namespace AK.Enova.API
{
    [ApiController]
    public abstract class EnovaControllerBase : ControllerBase, IDisposable
    {
        private readonly EnovaSessionScope _scope;

        protected Session Session => _scope.Session;

        protected KadryModule KadryModule => _scope.Session.GetKadry();

        protected HandelModule HandelModule => _scope.Session.GetHandel();

        protected BusinessModule BusinessModule => _scope.Session.GetBusiness();    

        protected CRMModule CRMModule => _scope.Session.GetCRM();   

        protected SrodkiTrwaleModule SrodkiTrwaleModule => _scope.Session.GetSrodkiTrwale();    

        protected CoreModule CoreModule => _scope.Session.GetCore();

        protected EnovaControllerBase(EnovaService service)
        {
            _scope = new EnovaSessionScope(service);
        }

        public void Dispose()
        {
            _scope.Dispose();

        }
    }
}
