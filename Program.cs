using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using NLog;

namespace DecodedConf.HelloWorld
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static int Main(string[] args)
        {
            // Print Current Version
            var asm = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            Logger.Debug($"v{fvi.ProductVersion}");

            using (var cts = new CancellationTokenSource())
            {
                Console.CancelKeyPress += (s, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

#if !DEBUG
                // Only check for updates if running release
                var updateTask = Updater.Create(cts.Token);
                updateTask.Start();
#endif

                // Start OWIN host 
                var url = "http://localhost:8080";
                Logger.Debug("Starting web server on {0}", url);
                var webServer = WebServer.Create(url, cts.Token);

                try
                {
                    webServer.Start();
                    webServer.Wait(cts.Token);
                }
                catch
                {
                    // swallow
                }
            }

            return 0;
        }
    }
}
