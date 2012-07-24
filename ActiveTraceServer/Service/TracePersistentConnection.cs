using System.Threading.Tasks;
using ActiveTraceServer.Components;
using SignalR;

namespace ActiveTraceServer.Service
{
    public class TracePersistentConnection : PersistentConnection
    {
        public static readonly string Authenticated = "authenticated";
        public static readonly string Unauthenticated = "unauthenticated";

        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            return Groups.Add(connectionId, Unauthenticated);
        }

        protected override Task OnDisconnectAsync(string connectionId)
        {
            return Groups.Remove(connectionId, Authenticated)
                  .ContinueWith(task => Groups.Remove(connectionId, Unauthenticated));
        }

        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            if (ClientTokens.Instance.Exists(data))
            {
                return Groups.Add(connectionId, Authenticated)
                      .ContinueWith(task => Groups.Remove(connectionId, Unauthenticated));
            }
            else
            {
                return base.OnReceivedAsync(request, connectionId, data);
            }
        }
    }
}