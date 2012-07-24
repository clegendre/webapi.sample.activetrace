using System;
using System.Web.Http;
using ActiveTraceServer.Components;

namespace ActiveTraceServer.Service
{
    /// <summary>
    /// authenticate controller gives out token for authorized clients
    /// </summary>
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
    }
}