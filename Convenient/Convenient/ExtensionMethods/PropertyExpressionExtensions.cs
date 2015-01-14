using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Convenient.ExtensionMethods
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

        public static string GetMemberName<TProperty>(this Expression<Func<TProperty>> expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }
            var member = GetMemberExpressionFrom(expression.Body);
            return member.Member.Name;
        }

        public static string GetMemberName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }
            var member = GetMemberExpressionFrom(expression.Body);
            return member.Member.Name;
        }

        public static PropertyInfo GetProperty<TModel, TProperty>(this Expression<Func<TModel, TProperty>> expression)
        {
            var member = GetMemberExpressionFrom(expression.Body).Member;
            if (member is PropertyInfo)
            {
                return (PropertyInfo)member;
            }
            throw new InvalidOperationException(string.Format("{0} is a {1}, not a property", expression.Body, member.GetType().GetFriendlyName()));
        }

        private static MemberExpression GetMemberExpressionFrom(Expression expression)
        {
            return DoGetMemberExpressionFrom((dynamic)expression);
        }

        private static MemberExpression DoGetMemberExpressionFrom(UnaryExpression expression)
        {
            return GetMemberExpressionFrom((dynamic)expression.Operand);
        }

        private static MemberExpression DoGetMemberExpressionFrom(MemberExpression expression)
        {
            return expression;
        }

        private static MemberExpression DoGetMemberExpressionFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to get a MemberExpression from {0}", invalid.GetType().Name));
        }
    }
}