using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotNetPrograms.Common.Exceptions;
using DotNetPrograms.Common.ExtensionMethods;

namespace DotNetPrograms.Common.Validation
{
    public class ModelValidator<TModel> : IModelValidator
    {
        private readonly TModel _model;

        public IEnumerable<string> ErrorMessages { get { return Errors.Select(e => e.ToString()); } }
        public bool HasErrors { get { return Errors.Any(); } }
        public IList<PropertyError> Errors { get; private set; }

        public ModelValidator(TModel model)
        {
            Errors = new List<PropertyError>();
            _model = model;
        }

        private static bool IsSet(object value)
        {
            return value != null;
        }

        private static bool IsSet(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private static bool IsSet<T>(T value) where T : struct
        {
            return !default(T).Equals(value);
        }

        public ModelValidator<TModel> Append(IModelValidator modelValidator)
        {
            Errors = Errors.Concat(modelValidator.Errors).ToList();
            return this;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression, string customErrorMessage = null)
        {
            var value = expression.Compile().Invoke(_model);
            if (!IsSet((dynamic)value))
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(customErrorMessage, string.Format((string)"{0} er påkrevd", (object)name)));
            }
            return this;
        }

        private void AddError(string propertyName, string message)
        {
            Errors.Add(new PropertyError(propertyName, message));
        }

        public ModelValidator<TModel> RequireAtLeast<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty minValue, string customErrorMessage = null)
            where TProperty : IComparable<TProperty>
        {
            var value = expression.Compile().Invoke(_model);
            if (value.CompareTo(minValue) < 0)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(customErrorMessage, string.Format("{0} må være minst {1}", name, minValue)));
            }
            return this;
        }

        public ModelValidator<TModel> RequireAtMost<TProperty>(Expression<Func<TModel, TProperty>> expression, TProperty maxValue, string customErrorMessage = null)
            where TProperty : IComparable<TProperty>
        {
            var value = expression.Compile().Invoke(_model);
            if (value.CompareTo(maxValue) > 0)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(customErrorMessage, string.Format("{0} kan ikke være større enn {1}", name, maxValue)));
            }
            return this;
        }

        public ModelValidator<TModel> RequireLengthMax(Expression<Func<TModel, string>> expression, int maxLength, string errorMessage = null)
        {
            var value = expression.Compile().Invoke(_model);
            if (value == null)
            {
                return this;
            }
            if (value.Length > maxLength)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(errorMessage, string.Format("{0} må være kortere enn {1} tegn.", value, maxLength)));
            }
            return this;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression,
            IPropertyValidator<TProperty> validator,
            string errorMessage = null)
        {
            var value = expression.Compile().Invoke(_model);
            try
            {
                validator.Validate(value);
            }
            catch (Exception ex)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(errorMessage, ex.Message));
            }
            return this;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression,
            Action<TProperty> validate,
            string errorMessage = null)
        {
            var value = expression.Compile().Invoke(_model);
            try
            {
                validate(value);
            }
            catch (Exception ex)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(errorMessage, ex.Message));
            }
            return this;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression,
            Action<TModel> validate,
            string errorMessage = null)
        {
            try
            {
                validate(_model);
            }
            catch (Exception ex)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(errorMessage, ex.Message));
            }
            return this;
        }

        public ModelValidator<TModel> Require<TProperty>(Expression<Func<TModel, TProperty>> expression,
            Action validate,
            string errorMessage = null)
        {
            try
            {
                validate();
            }
            catch (Exception ex)
            {
                var name = expression.GetPropertyName();
                AddError(name, CustomOrDefaultMessage(errorMessage, ex.Message));
            }
            return this;
        }

        public ModelValidator<TModel> RequireConditionally<TProperty>(Expression<Func<TModel, TProperty>> expression, IPropertyValidator<TProperty> validator,
            bool condition,
            string errorMessage = null)
        {
            if (condition)
            {
                return Require(expression, validator, errorMessage);
            }
            return this;
        }

        public ModelValidator<TModel> RequireConditionally<TProperty>(Expression<Func<TModel, TProperty>> expression,
           bool condition,
           string errorMessage = null)
        {
            if (condition)
            {
                return Require(expression, errorMessage);
            }
            return this;
        }

        private static string CustomOrDefaultMessage(string customMessage, string defaultMessage)
        {
            return customMessage.IsNullOrWhiteSpace()
                       ? defaultMessage
                       : customMessage;
        }

        public void OrThrowPropertyError()
        {
            if (HasErrors)
            {
                throw new PropertyErrorException(Errors);
            }
        }
    }
}