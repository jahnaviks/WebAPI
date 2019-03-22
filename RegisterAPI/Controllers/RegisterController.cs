using Newtonsoft.Json;
using RegisterAPI.Models;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RegisterAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegisterController : ApiController
    {
        RegisterService regServ;
        public RegisterController()
        {
            regServ = new RegisterService();
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/Register/ViewList")]
        public IHttpActionResult ViewList()
        {
            var result = regServ.GetAccountDetails();
            return Ok<IList<Registration>>(result);
        }

        [HttpGet]
        [Route("api/Register/ValidateLogin")]
        public IHttpActionResult ValidateLogin(string email)
        {
            var result = regServ.ValidateLogin(email);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Register/Register")]
        public void Register(HttpRequestMessage request)
        {
            string rr = request.Content.ReadAsStringAsync().Result;
            Registration regs = JsonConvert.DeserializeObject<Registration>(rr);
            regServ.RegisterDetails(regs);
        }

        [HttpGet]
        [Route("api/Register/GetUserDetails")]
        public IHttpActionResult GetUserDetails(string email)
        {
            var result = regServ.GetUserDetails(email);
            return Ok<Registration>(result);
        }

        [Route("api/Register/Update")]
        public void Update(HttpRequestMessage request)
        {
            string rr = request.Content.ReadAsStringAsync().Result;
            Registration regs = JsonConvert.DeserializeObject<Registration>(rr);
            regServ.UpdateClick(regs);
        }

        [HttpGet]
        [Route("api/Register/Delete")]
        public void Delete(string email)
        {
            regServ.DeleteClick(email);
        }



        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "GET,POST,OPTIONS,DELETE,PUT")]
        [Route("api/Register/PostRegister")]
        public void PostRegister([FromBody] RegisterAPI.Models.Registration employee)
        {
            regServ.RegisterDetails(employee);
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "GET,POST,OPTIONS,DELETE,PUT")]
        [Route("api/Register/PutUpdate")]
        public void PutUpdate([FromBody] RegisterAPI.Models.Registration employee)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RegisterDBEntities"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Register set FirstName='"+employee.FirstName+"',EmailId='"+employee.Email+"', LastName='"+employee.LastName+"', MobileNumber='"+employee.PhoneNumber+"', Address='"+employee.Address+"', Password='"+employee.Password+"' where id="+employee.id+"", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "GET,POST,OPTIONS,DELETE,PUT")]
        [Route("api/Register/DelRegister")]
        public void DelRegister([FromBody] RegisterAPI.Models.Registration employee)
        {
            string email = employee.Email;
            regServ.DeleteClick(email);
        }


    }
}
