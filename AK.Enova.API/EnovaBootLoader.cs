using Soneta.Business.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public static class EnovaBootLoader
    {
        private static bool _initialized;

        public static void Init()
        {
            if (_initialized) return;

            BusApplication.Initialize();
            _initialized = true;
        }
    }
}
