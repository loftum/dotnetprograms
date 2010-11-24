using System;

namespace Read
{
    public class MovieReaderException : Exception
    {
        public MovieReaderException(string message) : base(message)
        {
        }

        public MovieReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}