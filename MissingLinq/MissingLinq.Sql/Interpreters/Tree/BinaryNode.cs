using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class BinaryNode : SqlTreeNode<BinaryExpression>
    {
        private readonly ITreeNode _left;
        private readonly ITreeNode _right;
        private readonly string _operator;

        public BinaryNode(MissingLinqSql sql, BinaryExpression expression) : base(sql, expression)
        {
            _left = For(sql, expression.Left);
            _right = For(sql, expression.Right);
            _operator = new SqlOperatorTranslator().Translate(Expression.NodeType);
        }

        public override string Translate()
        {
            return string.Format("({0} {1} {2})", _left.Translate(), _operator, _right.Translate());
        }
    }
}