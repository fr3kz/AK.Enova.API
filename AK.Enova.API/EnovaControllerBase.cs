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
    [ServiceFilter(typeof(EnovaSessionFilter))]
    public abstract class EnovaControllerBase : ControllerBase
    {
        protected Session Session =>
            HttpContext.Items["ENOVA_SESSION"] as Session
            ?? throw new Exception("Brak sesji Enovy");

        protected KadryModule KadryModule => Session.GetKadry();
        protected HandelModule HandelModule => Session.GetHandel();
        protected BusinessModule BusinessModule => Session.GetBusiness();
        protected CRMModule CRMModule => Session.GetCRM();
        protected SrodkiTrwaleModule SrodkiTrwaleModule => Session.GetSrodkiTrwale();
        protected CoreModule CoreModule => Session.GetCore();
    }
}
