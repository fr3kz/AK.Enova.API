using Soneta.Business.App;
using Soneta.Start;
using System;
using System.Collections.Generic;
using System.IO;
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
            
            loader.UseCurrentDllsAsInstallation(true);

            var baseDir = AppContext.BaseDirectory;
            baseDir += @"dll\";

            if (Path.Exists(baseDir))
            {
                var files = Directory.GetFiles(baseDir, "*.dll");

                foreach (var file in files)
                {
                    var dllFullPath = Path.Combine(baseDir,file);

                    if (File.Exists(dllFullPath))
                    {
                        loader.AddPrivateFileExtension(dllFullPath);
                    }

                }
            }

         

            loader.Load();

            


            _initialized = true;
        }
    }
}
