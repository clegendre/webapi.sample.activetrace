using System.Collections.Concurrent;
using System.Linq;

namespace ActiveTraceServer.Components
{
    /// <summary>
    /// stoarge of the client tokens, used to authenticate clients
    /// 
    /// improvement: expire token after a period of time
    /// </summary>
    public class ClientTokens
    {
        private static ClientTokens s_instnace = new ClientTokens();
        private ConcurrentBag<string> _tokens = new ConcurrentBag<string>();

        private ClientTokens()
        {
        }

        public static ClientTokens Instance
        {
            get
            {
                return s_instnace;
            }
        }

        public bool Exists(string token)
        {
            return _tokens.Contains(token);
        }

        public void AddToken(string token)
        {
            _tokens.Add(token);
        }
    }
}