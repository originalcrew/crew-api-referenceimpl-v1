using System;
using System.Runtime.Serialization;

namespace Crew.Api.ReferenceImpl.V1.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException()
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}