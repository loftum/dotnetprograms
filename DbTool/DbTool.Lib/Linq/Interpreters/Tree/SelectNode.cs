using System.Collections.Generic;
using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class SelectNode : MethodCallNode
    {
        private readonly IList<string> _columns;
        private readonly LambdaSelector _lambda;
        private readonly ITreeNode _nextNode;

        public SelectNode(ITreeNode parent, MethodCallExpression expression) : base(parent, expression)
        {
            _lambda = new LambdaSelector(Expression.Arguments[1].GetLambda());
            _columns = _lambda.Properties;
            _nextNode = For(this, expression.Arguments[0]);
        }

        public override string Translate()
        {
            return string.Format("select {0} {1}", string.Join(", ", _columns), _nextNode.Translate());
        }
    }
}