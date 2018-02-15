using System;
using System.Runtime.Serialization;

namespace WTS.BL.Exceptions
{
    public class AppDbCommonException : Exception
    {
        public AppDbCommonException()
        {
        }

        public AppDbCommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AppDbCommonException(string message)
            : base(message)
        {
        }

        protected AppDbCommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
