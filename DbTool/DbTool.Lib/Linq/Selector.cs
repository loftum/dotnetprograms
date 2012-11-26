using System;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class Selector
    {
        private readonly MethodCallExpression _expression;
        private readonly Type _tableType;

        public Selector(MethodCallExpression expression)
        {
            _expression = expression;
            _tableType = expression.Type.GenericTypeArguments[0];
        }

        public DbToolSql GetSql()
        {
            var result = new DbToolSql();
            result.Append(string.Format("select * from {0}", _tableType.Name));
            return result;
        }
    }
}