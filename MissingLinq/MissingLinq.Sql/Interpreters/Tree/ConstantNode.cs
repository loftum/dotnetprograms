using System.Linq;
using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class ConstantNode : SqlTreeNode<ConstantExpression>
    {
        private readonly object _value;
        public ConstantNode(MissingLinqSql sql, ConstantExpression expression) : base(sql, expression)
        {
            if (typeof (IQueryable).IsAssignableFrom(Expression.Type))
            {
                sql.Table = Expression.Type.GenericTypeArguments[0].Name;
                _value = string.Empty;
            }
            else
            {
                var parameter = sql.NewParameter();
                parameter.Value = Expression.Value;
                _value = parameter.Name;
            }
        }

        public override string Translate()
        {
            return _value.ToString();
        }
    }
}