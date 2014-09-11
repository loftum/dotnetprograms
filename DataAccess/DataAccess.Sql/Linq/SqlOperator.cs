using System;
using System.Linq.Expressions;

namespace DataAccess.Sql.Linq
{
    public static class SqlOperator
    {
        public static string For(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return "and";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "or";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return @"/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Not:
                    return "not";
                case ExpressionType.Quote:
                    return "";
            }
            throw new InvalidOperationException(string.Format("Unknown operator {0}", type));
        } 
    }
}