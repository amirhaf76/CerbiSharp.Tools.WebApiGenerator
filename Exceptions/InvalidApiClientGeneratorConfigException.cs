using System.Runtime.Serialization;

namespace CerbiSharp.Tools.WebApiGenerator.Exceptions
{
    [Serializable]
    internal class InvalidApiClientGeneratorConfigException : Exception
    {
        public InvalidApiClientGeneratorConfigException()
        {
        }

        public InvalidApiClientGeneratorConfigException(string? message) : base(message)
        {
        }

        public InvalidApiClientGeneratorConfigException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidApiClientGeneratorConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}