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
    public class CustomerController : ApiController
    {
        ClsDB db = ClsDB.Instance;

        // GET: api/Customer/5
        public RequestResponse Get(string gmail, string password)
        {
            return db.GetCustomerResponse(gmail, password);
        }

        public void Post(Customer customer)
        {
            db.NewCustomer(customer);
        }












        //----------delete--------
        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }
        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
