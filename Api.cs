namespace Microsoft.Toyota.Web
{
    using System.Collections.Generic;
    using System.Web.Http;

    public class SpeechController : ApiController
    {
        // GET api/speech
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        // GET api/speech/5 
        public string Get(int id)
        {
            return "Hello, World!";
        }

        // POST api/speech 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/speech/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/speech/5 
        public void Delete(int id)
        {
        }
    }
}