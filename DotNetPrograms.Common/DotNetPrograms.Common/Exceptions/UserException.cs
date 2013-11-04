using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.Describing;

namespace DotNetPrograms.Common.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        public static UserException Unknown<T>(params Expression<Func<object>>[] properties)
        {
            return new UserException("There is no {0}", Description.Of<T>(properties).ToString());
        }
    }
}