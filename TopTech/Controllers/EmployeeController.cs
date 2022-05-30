using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Bll;

namespace TopTech.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        ClsDB db = ClsDB.Instance;

        // GET: api/Employee/5/5
        public RequestResponse Get(string gmail, string password)
        {
            return db.GetEmployeeResponse(gmail, password);
        }

        //return all employees
        public RequestResponse Get()
        {
            return db.GetEmployeesResponse();
        }












        //----------delete--------
        // POST: api/Employee
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }
    }
}
