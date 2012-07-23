using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ActiveTraceServer.Service;
using SignalR;

namespace ActiveTraceServer
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            config.Services.Replace(typeof(ITraceWriter), TraceWriter.Instance);

            config.Routes.MapHttpRoute(
                "default",
                "api/{controller}/{action}",
                new { action = RouteParameter.Optional });

            RouteTable.Routes.MapConnection<TraceEndpoint>("trace", "trace/{*operatopn}");
        }
    }
}