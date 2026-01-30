using Microsoft.AspNetCore.Mvc;
using Soneta.Business;

namespace AK.Enova.API
{
    [ApiController]
    public abstract class EnovaControllerBase : ControllerBase
    {
        private readonly EnovaSessionScope _scope;

        protected Session Session => _scope.Session;

        protected EnovaControllerBase(EnovaService service)
        {
            _scope = new EnovaSessionScope(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _scope.Dispose();

            base.Dispose(disposing);
        }
    }
}
