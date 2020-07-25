using System;
using System.Runtime.Serialization;

namespace IRCgirld.Exceptions
{
	[Serializable]
	internal class ServiceNotFoundException : Exception
	{
		private Type type;

		public ServiceNotFoundException()
		{
		}

		public ServiceNotFoundException(Type type) : base($"Service of type {type.Name} not registed in this service lcoator.")
		{
			this.type = type;
		}

		public ServiceNotFoundException(string message) : base(message)
		{
		}

		public ServiceNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ServiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}