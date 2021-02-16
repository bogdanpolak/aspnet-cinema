using System;
using System.Runtime.Serialization;

namespace Data.Cinema.Errors
{
    [Serializable]
    internal class ShowNotExistsException : Exception
    {
        public ShowNotExistsException()
        {
        }

        public ShowNotExistsException(string message) : base(message)
        {
        }

        public ShowNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShowNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}