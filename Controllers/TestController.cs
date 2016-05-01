using System.Web.Http;

namespace DecodedConf.HelloWorld.Controllers
{
    public class TestController : ApiController
    {
        // GET api/test
        public IHttpActionResult Get()
        {
            return Ok("hello");
        }
    }
}