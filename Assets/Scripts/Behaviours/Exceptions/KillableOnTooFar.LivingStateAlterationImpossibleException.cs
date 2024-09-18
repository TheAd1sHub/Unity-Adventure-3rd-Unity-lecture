using System;
using System.Runtime.Serialization;

namespace MegaChaseGame.Behaviours
{
	public sealed partial class KillableOnTooFar
	{
		public sealed class LivingStateAlterationImpossibleException : ActionImpossibleException
		{
			public LivingStateAlterationImpossibleException()
				: base() { }

			public LivingStateAlterationImpossibleException(string message)
				: base(message) { }

			public LivingStateAlterationImpossibleException(SerializationInfo info, StreamingContext context)
				: base(info, context) { }

			public LivingStateAlterationImpossibleException(string message, Exception innerException)
				: base(message, innerException) { }
		}
	}
}
