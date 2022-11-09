using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace csharp_gestore_eventi.Exceptions
{
    public class GestoreEventiException : Exception
    {
        public GestoreEventiException()
        {
        }

        public GestoreEventiException(string? message) : base(message)
        {
        }

        public GestoreEventiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GestoreEventiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
