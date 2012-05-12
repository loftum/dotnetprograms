using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Exceptions;

namespace BuildMonitor.Lib.Validation
{
    public class ModelValidator<TModel>
    {
        private readonly TModel _model;

        public IEnumerable<string> Errors { get { return _errors; } }
        private readonly IList<string> _errors = new List<string>();

        public bool IsValid
        {
            get { return !_errors.Any(); }
        }

        public ModelValidator(TModel model)
        {
            _model = model;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression)
            where TProperty : class
        {
            var value = expression.Compile().Invoke(_model);
            if (value == null)
            {
                var name = expression.GetPropertyName();
                _errors.Add(string.Format("{0} is required", name));
            }
            return this;
        }

        public ModelValidator<TModel> Require(params Expression<Func<TModel, string>>[] expressions)
        {
            foreach (var expression in expressions)
            {
                Require(expression);
            }
            return this;
        }

        public ModelValidator<TModel> Require(Expression<Func<TModel, string>> expression)
        {
            var value = expression.Compile().Invoke(_model);
            if (string.IsNullOrWhiteSpace(value))
            {
                var name = expression.GetPropertyName();
                _errors.Add(string.Format("{0} is required", name));
            }
            return this;
        }

        public ModelValidator<TModel> RequireAtLeast<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty minValue)
            where TProperty : IComparable<TProperty>
        {
            var value = expression.Compile().Invoke(_model);
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
                throw new ValidationException("Validation failed: {0}", string.Join(", ", _errors));
            }
        }
    }
}