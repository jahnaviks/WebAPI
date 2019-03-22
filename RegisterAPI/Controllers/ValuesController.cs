using RegisterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RegisterAPI.Controllers
{
    public class ValuesController : ApiController
    {
        RegisterService regServ;
        public ValuesController()
        {
            regServ = new RegisterService();
        }
       // RegisterAPI.RegisterService.RegisterService regServ;
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult ViewList()
        {
            var result = regServ.GetAccountDetails();
            return Ok<IList<Registration>>(result);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}