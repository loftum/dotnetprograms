using System;
using BasicManifest.Common.Describing;

namespace BasicManifest.Common.Exceptions
{
    public class BMException : Exception
    {
        public BMException(string message) : base(message)
        {
        }

        public static BMException Unknown<T>()
        {
            return new BMException(Describe.Type<T>());
        }
    }
}