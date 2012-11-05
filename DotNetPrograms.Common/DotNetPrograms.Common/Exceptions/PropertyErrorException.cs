using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.Exceptions
{
    public class PropertyErrorException : UserException
    {
        public IEnumerable<PropertyError> Errors { get; private set; }

        public PropertyErrorException(string property, string errorMessage)
             : this(new List<PropertyError> { new PropertyError(property, errorMessage) })
        {
        }

        public PropertyErrorException(IList<PropertyError> errors)
            : base(string.Join(Environment.NewLine, errors.Select(e => e.PropertyName + " => " + e.ErrorMessage).ToArray()))
        {
            Errors = errors;
        }

        public override string Message
        {
            get
            {
                return string.Join(Environment.NewLine, Errors.Select(e => e.PropertyName + " => " + e.ErrorMessage).ToArray());
            }
        }
    }
}