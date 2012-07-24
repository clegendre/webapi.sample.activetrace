using System;
using System.Web.Http;

namespace ActiveTraceServer.Service
{
    public class TraceAuthenticationController : ApiController
    {
        [Authorize(Roles = "admin")]
        [AcceptVerbs("POST")]
        public string Authenticate()
        {
            return Guid.NewGuid().ToString();
        }

        [AcceptVerbs("GET")]
        public string Version()
        {
            return "0.1";
        }
    }
}