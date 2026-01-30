using Microsoft.AspNetCore.Mvc;
using Soneta.Business;
using System;

namespace AK.Enova.API
{
    [ApiController]
    public abstract class EnovaControllerBase : ControllerBase, IDisposable
    {
        private readonly EnovaSessionScope _scope;

        protected Session Session => _scope.Session;

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
