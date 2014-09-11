using System;
using System.Linq.Expressions;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Linq
{
    public static class ExpressionValueRetreiver
    {
        public static object GetValueFrom(Expression expression)
        {
            return expression == null ? null : DoGetvalueFrom((dynamic) expression);
        }

        private static object DoGetvalueFrom(ConstantExpression expression)
        {
            return expression.Value;
        }

        private static object DoGetvalueFrom(MemberExpression expression)
        {
            var owner = GetValueFrom(expression.Expression);
            return expression.Member.GetValue(owner);
        }

        private static object DoGetvalueFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to get value from {0}", invalid.GetType().GetFriendlyName()));
        }
    }
}