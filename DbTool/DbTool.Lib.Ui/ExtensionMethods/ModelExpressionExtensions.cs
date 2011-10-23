using System;
using System.Linq.Expressions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Ui.ExtensionMethods
{
    public static class ModelExpressionExtensions
    {
        public static string GetPropertyId<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
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

        public static string GetPropertyId<TProperty>(this Expression<Func<TProperty>> expression)
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