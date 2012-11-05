using System;
using System.Linq.Expressions;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class PropertyExpressionExtensions
    {
        public static string GetPropertyName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }

            var parameter = expression.Parameters[0].Name;
            var pattern = string.Format("{0}.", parameter);
            var body = expression.Body.ToString();
            return body.RemoveFirstMatch(pattern);
        }

        public static string GetPropertyName<TProperty>(this Expression<Func<TProperty>> expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }

            const string selfReference = @"value\({1}[\w\W]+\){1}\.";
            var body = expression.Body.ToString();
            return body.RemoveFirstMatch(selfReference);
        } 
    }
}