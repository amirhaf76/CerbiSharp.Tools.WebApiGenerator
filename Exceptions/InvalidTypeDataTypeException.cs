using System.Runtime.Serialization;

namespace CerbiSharp.Tools.WebApiGenerator.Exceptions
{
    [Serializable]
    public class InvalidTypeDataTypeException : Exception
    {
        public InvalidTypeDataTypeException()
        {
        }

        public InvalidTypeDataTypeException(string? message) : base(message)
        {
        }

        public InvalidTypeDataTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidTypeDataTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}