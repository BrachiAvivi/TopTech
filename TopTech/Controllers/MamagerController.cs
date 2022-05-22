using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TopTech.Controllers
{
    public class MamagerController : ApiController
    {
        // GET: api/Mamager
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Mamager/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mamager
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mamager/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mamager/5
        public void Delete(int id)
        {
        }
    }
}
