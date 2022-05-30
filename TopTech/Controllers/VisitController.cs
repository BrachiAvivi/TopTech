using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Dto;

namespace TopTech.Controllers
{
    public class VisitController : ApiController
    {
        ClsDB db = ClsDB.Instance;
        // GET: api/Visit
        public RequestResponse Get(Employee employee)
        {
            return db.GetVisitsResponse(employee);
        }


       
        
        
        
        
        
        
        
        
        
        
        //----------delete--------

        // GET: api/Visit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Visit
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Visit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Visit/5
        public void Delete(int id)
        {
        }
    }
}
