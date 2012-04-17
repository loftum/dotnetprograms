using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.Common.ExtensionMethods;

namespace VisualFarmStudio.Common.Validation
{
    public class InputValidator
    {
        public IEnumerable<string> Errors { get { return _errors; } }
        private readonly IList<string> _errors = new List<string>();

        public InputValidator Require<TProperty>(Expression<Func<TProperty>> expression)
            where TProperty : class
        {
            var value = expression.Compile().Invoke();
            if (value == null)
            {
                var name = expression.GetPropertyName();
                _errors.Add(string.Format("{0} is required", name));
            }
            return this;
        }

        public InputValidator Require(Expression<Func<string>> expression)
        {
            var value = expression.Compile().Invoke();
            if (string.IsNullOrWhiteSpace(value))
            {
                var name = expression.GetPropertyName();
                _errors.Add(string.Format("{0} is required", name));
            }
            return this;
        }

        public InputValidator RequireAtLeast<TProperty>(Expression<Func<TProperty>> expression, TProperty minValue)
            where TProperty : IComparable<TProperty>
        {
            var value = expression.Compile().Invoke();
            if (value.CompareTo(minValue) < 0)
            {
                var name = expression.GetPropertyName();
                _errors.Add(string.Format("{0} must be at least {1}", name, minValue));
            }
            return this;
        }

        public void OrThrow()
        {
            if (_errors.Any())
            {
                throw new UserException(ExceptionType.InvalidInput, string.Join(", ", _errors));
            }
        }
    }
}