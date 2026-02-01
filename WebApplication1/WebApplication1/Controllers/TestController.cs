using AK.Enova.API;
using Microsoft.AspNetCore.Mvc;
using Soneta.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/test")]
    public class TestController : EnovaControllerBase
    {
        public TestController(EnovaService enova)
           
        {
        }

        [HttpGet]
        public string Test()
        {
            return
                $"Operator: {Session.Login.OperatorName} | " +
                $"Baza: {Session.Login.Database.Name}";
        }

        [HttpPost]
        public string TestPost([FromBody] TestRequest request)
        {
            
            using(var t = Session.Logout(true))
            {
                var kth = new Kontrahent();
                t.Session.AddRow(kth);
                kth.Nazwa = $"{request.Imie} {request.Nazwisko}";
                t.Commit();
              
            }

            Session.Save();

            return $"OK";
        
        }
    }
}
