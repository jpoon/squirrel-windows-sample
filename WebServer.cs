using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace DecodedConf.HelloWorld
{
    public class WebServer
    {
        public static Task Create(string url, CancellationToken token)
        {
            return new Task(() =>
            {
                using (WebApp.Start<Startup>(url))
                {
                    token.WaitHandle.WaitOne();
                }
            }, token, TaskCreationOptions.LongRunning); 
        }
    }
}
