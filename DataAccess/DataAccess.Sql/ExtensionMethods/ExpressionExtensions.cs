using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class ExpressionExtensions
    {
        public static Expression StripQuotes(this Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }

        public static LambdaExpression GetLambda(this Expression expression)
        {
            var lambda = StripQuotes(expression) as LambdaExpression;
            if (lambda == null)
            {
                var constantValue = (ConstantExpression)expression;
                lambda = (LambdaExpression)constantValue.Value;
            }
            return lambda;
        }

        public static Expression GetArgumentOrDefault(this MethodCallExpression expression, int index)
        {
            return expression.Arguments.Count > index
                ? expression.Arguments[index]
                : null;
        }

        public static object GetValue(this MemberExpression expression)
        {
            var constant = expression.Expression as ConstantExpression;
            if (constant == null)
            {
                throw new InvalidOperationException(string.Format("Don't know how to get value from {0}", expression.Expression.GetType().GetFriendlyName()));
            }
            var member = expression.Member;
            if (member is FieldInfo)
            {
                var field = (FieldInfo) member;
                return field.GetValue(constant.Value);
            }
            if (member is PropertyInfo)
            {
                var property = (PropertyInfo) member;
                return property.GetValue(constant.Value);
            }
            throw new InvalidOperationException(string.Format("Don't know how to get value from {0}", member.GetType().GetFriendlyName()));
        }
    }
}