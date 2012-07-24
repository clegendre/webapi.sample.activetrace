using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Web.Http.Tracing;
using SignalR;

namespace ActiveTraceServer.Service
{
    /// <summary>
    /// writes the traces to the persistent connection
    /// </summary>
    public class TraceWriter : ITraceWriter
    {
        private ConcurrentQueue<TraceRecord> _traces;

        private TraceWriter()
        {
            _traces = new ConcurrentQueue<TraceRecord>();
        }

        private static TraceWriter s_instance = new TraceWriter();

        public static TraceWriter Instance
        {
            get { return s_instance; }
        }

        public void Trace(
            HttpRequestMessage request,
            string category,
            TraceLevel level,
            Action<TraceRecord> traceAction)
        {
            var trace = new TraceRecord(request, category, level);
            traceAction(trace);

            _traces.Enqueue(trace);

            var context = GlobalHost.ConnectionManager.GetConnectionContext<TracePersistentConnection>();
            context.Groups.Send(
                TracePersistentConnection.Authenticated,
                new
                {
                    trace.RequestId,
                    trace.Request.RequestUri,
                    Status = trace.Status.ToString(),
                    Level = trace.Level.ToString(),
                    trace.Message,
                    trace.Category,
                    TimeTicks = trace.Timestamp.Ticks,
                    trace.Operator,
                    trace.Operation,
                    Exception = trace.Exception == null ? "" : trace.Exception.Message,
                });
        }
    }
}