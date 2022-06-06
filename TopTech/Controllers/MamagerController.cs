using Bll;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace TopTech.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MamagerController : ApiController
    {
        ClsDB db = ClsDB.Instance;
        // GET: api/Mamager
        public bool Get(string password)
        {
            return db.ManagerEnter(password);
        }


        // POST: api/Mamager
        public void Post()
        {
            OpenBusinessDay algoritem = new OpenBusinessDay();
            algoritem.OpenDay();
        }










        //----------delete--------
        // GET: api/Mamager/5




        // PUT: api/Mamager/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Mamager/5
        public void Delete(int id)
        {
        }
    }
}
