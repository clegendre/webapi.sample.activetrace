using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActiveTraceServer.Service
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;

            if (auth != null && auth.Scheme == "Base")
            {
                var encoder = Encoding.ASCII;

                string v = "admin:password";
                string ec = Convert.ToBase64String(encoder.GetBytes(v));

                var raw = encoder.GetString(Convert.FromBase64String(auth.Parameter));
                var splited = raw.Split(new char[] { ':' });
                var username = splited[0];
                var password = splited[1];

                if ((username == "admin" && password == "password") ||
                    (username == "poweruser" && password == "power"))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(username), new string[] { "admin" });
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}