using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public sealed class EnovaSessionFilter : IActionFilter
    {
        private readonly EnovaService _service;
        private EnovaSessionScope _scope;

        public EnovaSessionFilter(EnovaService service)
        {
            _service = service;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _scope = new EnovaSessionScope(_service);

            context.HttpContext.Items["ENOVA_SESSION"] = _scope.Session;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _scope?.Dispose();
        }
    }
}
