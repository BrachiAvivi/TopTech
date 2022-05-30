using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TopTech.Controllers
{
    public class ServiceController : ApiController
    {
        ClsDB db = ClsDB.Instance;
        // GET: api/Service
        public RequestResponse Get()
        {
            return db.GetServicesResponse();
        }












        //----------delete--------
        // GET: api/Service/5
        public object Get(int id)
        {
            return "abc";
        }

        // POST: api/Service
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Service/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Service/5
        public void Delete(int id)
        {
        }
    }
}
