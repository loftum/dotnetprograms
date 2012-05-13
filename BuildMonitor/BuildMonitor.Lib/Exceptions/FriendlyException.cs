using System;

namespace BuildMonitor.Lib.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException(string message, params object[] args) : base(string.Format(message, args))
        {
        }
    }
}