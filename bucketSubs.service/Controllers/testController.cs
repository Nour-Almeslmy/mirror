using BusinessLogicLayer.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace bucketSubs.service.Controllers
{
    public class testController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("test/forall")]
        public IHttpActionResult Get()
        {
            return Ok("allow anonymous");
        }

        [BasicAuthentication]
        [HttpGet]
        [Route("test/forauth")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;

            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("test/foradmin")]
        public IHttpActionResult GetForAuthAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return Ok("hello" + identity.Name + "Role :" + String.Join(",", roles.ToList()));
        }
    }
}
