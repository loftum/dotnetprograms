using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MissingLinq.Sql.Interpreters.Tree.Methods
{
    public abstract class StringMethodNode : SqlTreeNode<MethodCallExpression>
    {
        protected readonly string ColumnName;
        protected readonly object Argument;

        protected StringMethodNode(MissingLinqSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            var column = (PropertyInfo)GetMemberExpression(Expression).Member;
            var argument = (ConstantExpression)Expression.Arguments[0];
            if (column.PropertyType != typeof(string))
            {
                throw new InvalidOperationException(string.Format("Cannot evaluate {0}.{1}({2})", column.PropertyType, expression.Method.Name, argument));
            }
            var parameter = sql.NewParameter();
            parameter.Value = argument.Value;
            ColumnName = column.Name;
            Argument = parameter.Name;
        }

        private static MemberExpression GetMemberExpression(MethodCallExpression method)
        {
            return (MemberExpression)method.Object;
        }
    }
}