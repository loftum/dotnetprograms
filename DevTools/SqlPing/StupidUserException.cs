using System;

namespace SqlPing
{
    public class StupidUserException : Exception
    {
        public StupidUserException(string message) : base(message)
        {
        }
    }
}