using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

namespace ActiveTraceServer.Components
{
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