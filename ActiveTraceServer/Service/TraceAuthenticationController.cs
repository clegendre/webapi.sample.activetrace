using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using ActiveTraceServer.Components;

namespace ActiveTraceServer.Service
{
    public class TraceAuthenticationController : ApiController
    {
        [Authorize(Roles = "admin")]
        [AcceptVerbs("GET")]
        public string GetToken(string username)
        {
            var key = Guid.NewGuid().ToString() + ":" + username;
            ClientTokens.Instance.AddToken(key);

            return key;
        }

        [AcceptVerbs("GET")]
        public string Version()
        {
            return "0.1";
        }
    }
}