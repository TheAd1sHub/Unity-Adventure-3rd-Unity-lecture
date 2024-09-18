using System;
using System.Runtime.Serialization;

namespace MegaChaseGame.Behaviours
{
    public abstract class ActionImpossibleException : System.Exception
    {
        public ActionImpossibleException()
            : base() { }

        public ActionImpossibleException(string message)
            : base(message) { }

        public ActionImpossibleException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ActionImpossibleException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
