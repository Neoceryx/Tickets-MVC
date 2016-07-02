using System;
using System.Runtime.Serialization;

namespace App.BLL
{
    [Serializable]
    public class InvalidFileExtension : Exception
    {
        public InvalidFileExtension()
        {
        }

        public InvalidFileExtension(string message) : base(message)
        {
        }

        public InvalidFileExtension(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFileExtension(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}