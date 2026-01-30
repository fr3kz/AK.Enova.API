using Soneta.Business.App;
using Soneta.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK.Enova.API
{
    public static class EnovaBootLoader
    {
        private static bool _initialized = false;

        public static void Init()
        {
            if (Loader.IsLoaded) return;
            if (_initialized) return;


            var loader = new Soneta.Start.Loader() { WithUI = true, WithNet = false,WithExtensions=true }; 
            loader.Load();


            _initialized = true;
        }
    }
}
