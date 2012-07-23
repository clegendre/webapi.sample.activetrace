using System;
using System.Web.Http;

namespace ActiveTraceServer.Service
{
    public class TraceAuthenticationController : ApiController
    {
        [Authorize(Roles = "admin")]
        public string Authenticate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}