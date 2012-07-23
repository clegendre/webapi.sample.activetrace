using System.Threading.Tasks;
using SignalR;

namespace ActiveTraceServer.Service
{
    public class TraceEndpoint : PersistentConnection
    {
        protected override Task OnConnectedAsync(IRequest request, string connectionId)
        {
            return base.OnConnectedAsync(request, connectionId);
        }

        protected override Task OnDisconnectAsync(string connectionId)
        {
            return base.OnDisconnectAsync(connectionId);
        }

        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }
    }
}