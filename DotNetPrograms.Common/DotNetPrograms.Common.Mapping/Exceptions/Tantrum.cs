using System;

namespace DotNetPrograms.Common.Mapping.Exceptions
{
    public class Tantrum : Exception
    {
        public Tantrum(string message) : base(message)
        {
        }
    }
}