using System;
using System.Runtime.Serialization;

namespace MegaChaseGame.Behaviours
{
    public sealed partial class Moving
    {
        public class MovementImpossibleException : ActionImpossibleException
        {
            public MovementImpossibleException()
                : base() { }

            public MovementImpossibleException(string message)
                : base(message) { }

            public MovementImpossibleException(string message, Exception innerException)
                : base(message, innerException) { }

            protected MovementImpossibleException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }
        }
    }
}
