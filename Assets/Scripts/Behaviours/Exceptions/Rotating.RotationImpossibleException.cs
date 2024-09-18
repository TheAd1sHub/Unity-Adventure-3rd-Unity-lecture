using System;
using System.Runtime.Serialization;

namespace MegaChaseGame.Behaviours
{
    public sealed partial class Rotating
    {
        public class RotationImpossibleException : ActionImpossibleException
        {
            public RotationImpossibleException()
                : base() { }

            public RotationImpossibleException(string message)
                : base(message) { }

            public RotationImpossibleException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }

            public RotationImpossibleException(string message, Exception innerException)
                : base(message, innerException) { }
        }
    }
}
