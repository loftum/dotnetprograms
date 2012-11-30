using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class BinaryNode : SqlTreeNode<BinaryExpression>
    {
        private readonly ITreeNode _left;
        private readonly ITreeNode _right;
        private readonly string _operator;

        public BinaryNode(ITreeNode parent, BinaryExpression expression) : base(parent, expression)
        {
            _left = For(this, expression.Left);
            _right = For(this, expression.Right);
            _operator = new SqlOperatorTranslator().Translate(Expression.NodeType);
        }

        public override string Translate()
        {
            return string.Format("({0} {1} {2})", _left.Translate(), _operator, _right.Translate());
        }
    }
}