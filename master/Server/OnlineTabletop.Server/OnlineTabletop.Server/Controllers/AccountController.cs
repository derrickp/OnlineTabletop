using OnlineTabletop.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTabletop.Server.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : ApiController
    {
        private IAccountManager<DbAccount> _accountManager;
        // GET: api/Account
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [AllowAnonymous]
        [Route("register")]
        public IHttpActionResult Post([FromBody]RegisterBindingModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbAccount = _accountManager.FindAccountByEmail(registerModel.Email);

            if (dbAccount != null)
            {
                return Conflict();
            }

            return Ok();
            

        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }

        public AccountController(IAccountManager<DbAccount> accountManager)
        {
            _accountManager = accountManager;
        }
    }
}
