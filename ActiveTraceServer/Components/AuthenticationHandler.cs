using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            if (auth == null || auth.Scheme != "Basic")
            {
                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                var resp = request.CreateResponse(HttpStatusCode.Unauthorized);
                resp.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic"));

                tcs.SetResult(resp);
                return tcs.Task;
            }
            else
            {
                var encoder = Encoding.GetEncoding("iso-8859-1");
                var raw = encoder.GetString(Convert.FromBase64String(auth.Parameter));
                var splited = raw.Split(new char[] { ':' });
                var username = splited[0];
                var password = splited[1];

                if (username == "admin" && password == "password")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity("Admin"), new string[] { "admin" });
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}