using System;
using System.Runtime.Serialization;

namespace App.BLL
{
    [Serializable]
    public class InvalidCredentials : Exception
    {
        public InvalidCredentials()
        {
        }

        public InvalidCredentials(string message) : base(message)
        {
        }

        public InvalidCredentials(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCredentials(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}