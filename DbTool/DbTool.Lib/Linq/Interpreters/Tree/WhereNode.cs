using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class WhereNode : SqlTreeNode<MethodCallExpression>
    {
        private readonly ITreeNode _condition;
        private readonly ITreeNode _previousNode;

        public WhereNode(ITreeNode parent, MethodCallExpression expression) : base(parent, expression)
        {
            _condition = For(this, Expression.Arguments[1].StripQuotes());
            _previousNode = For(this, Expression.Arguments[0]);
        }

        public override string Translate()
        {
            return string.Format("{0} where {1}", _previousNode.Translate(), _condition.Translate());
        }
    }
}