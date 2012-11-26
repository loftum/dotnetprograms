using System;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq
{
    public class Wherer
    {
        private Type _type;
        private readonly MethodCallExpression _expression;

        public Wherer(MethodCallExpression expression)
        {
            _expression = expression;
            _type = _expression.Type.GenericTypeArguments[0];
        }

        public DbToolSql GetSql()
        {
            var result = new DbToolSql();

            return result;
        }
    }
}