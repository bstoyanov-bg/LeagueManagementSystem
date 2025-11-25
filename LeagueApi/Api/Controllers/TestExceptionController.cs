using System;
using System.Web.Http;

namespace LeagueApi.Controllers
{
    [RoutePrefix("api/test")]
    public class TestExceptionController : ApiController
    {
        public TestExceptionController()
        {

        }

        [HttpGet, Route("")]
        public IHttpActionResult ThrowTestException()
        {
            throw new ArgumentNullException("Test argument null exception from controller!");
        }
    }
}