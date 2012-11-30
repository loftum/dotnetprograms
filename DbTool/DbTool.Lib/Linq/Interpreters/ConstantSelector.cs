using System;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters
{
    public class ConstantSelector
    {
        private readonly ConstantExpression _constant;
        private readonly Type _tableType;

        public ConstantSelector(ConstantExpression constant)
        {
            _constant = constant;
            _tableType = _constant.Type.GenericTypeArguments[0];
        }

        public DbToolSql GetSql()
        {
            var result = new DbToolSql();
            result.Append(string.Format("select * from {0}", _tableType.Name));
            return result;
        }
    }
}