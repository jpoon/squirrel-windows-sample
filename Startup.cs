using System.Linq;
using System.Web.Http;
using Owin;

namespace DecodedConf.HelloWorld
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "TestHttpRoute",
                routeTemplate: "api/test",
                defaults: new { controller = "test" }
            );

            // Json
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            app.UseWebApi(config);
        }
    }
}