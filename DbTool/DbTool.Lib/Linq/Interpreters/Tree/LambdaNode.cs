using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class LambdaNode : SqlTreeNode<LambdaExpression>
    {
        private readonly IList<ITreeNode> _parameters;
        private readonly ITreeNode _body;
        public LambdaNode(ITreeNode parent, LambdaExpression expression) : base(parent, expression)
        {
            _parameters = For(this, expression.Parameters);
            _body = For(this, expression.Body);
        }

        public override string Translate()
        {
            return _body.Translate();
        }

        public IList<string> GetProperties()
        {

            return null;
        }
    }
}