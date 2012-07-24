using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ActiveTraceServer.Service
{
    public class ValuesController : ApiController
    {
        public IEnumerable<int> Get()
        {
            return Enumerable.Range(0, 10);
        }

        public void ExceptionDemo()
        {
            throw new Exception("demo exception");
        }
    }
}