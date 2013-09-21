using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class ParameterNode : SqlTreeNode<ParameterExpression>
    {
        private readonly string _name;
        public ParameterNode(MissingLinqSql sql, ParameterExpression expression) : base(sql, expression)
        {
            _name = Expression.Name;
        }

        public override string Translate()
        {
            return _name;
        }
    }
}