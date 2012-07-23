using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ActiveTraceServer.Service
{
    public class ValuesController : ApiController
    {
        public IEnumerable<int> Get()
        {
            throw new Exception("exception");
            //return Enumerable.Range(0, 10);
        }
    }
}