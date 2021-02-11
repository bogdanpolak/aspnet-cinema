using System;
using System.Runtime.Serialization;

namespace Data.Cinema
{
    [Serializable]
    internal class MovieNotExistsException : Exception
    {
        public MovieNotExistsException()
        {
        }

        public MovieNotExistsException(string message) : base(message)
        {
        }

        public MovieNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MovieNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}