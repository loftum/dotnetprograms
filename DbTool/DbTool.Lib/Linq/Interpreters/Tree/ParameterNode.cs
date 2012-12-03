using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class ParameterNode : SqlTreeNode<ParameterExpression>
    {
        private readonly string _name;
        public ParameterNode(DbToolSql sql, ParameterExpression expression) : base(sql, expression)
        {
            _name = Expression.Name;
        }

        public override string Translate()
        {
            return _name;
        }
    }
}