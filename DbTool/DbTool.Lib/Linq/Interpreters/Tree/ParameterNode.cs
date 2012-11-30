using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class ParameterNode : SqlTreeNode<ParameterExpression>
    {
        public ParameterNode(ITreeNode parent, ParameterExpression expression) : base(parent, expression)
        {
        }

        public override string Translate()
        {
            return Expression.Name;
        }
    }
}