using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kangfupanda.webapi.Exceptions
{
    [System.Serializable]
    public class KangfuPandaException : Exception
    {
        public KangfuPandaException() { }
        public KangfuPandaException(string message) : base(message) { }
        public KangfuPandaException(string message, Exception inner) : base(message, inner) { }
        protected KangfuPandaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}