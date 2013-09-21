using System.Collections.Generic;
using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class LambdaNode : SqlTreeNode<LambdaExpression>
    {
        private readonly IList<ITreeNode> _parameters;
        private readonly ITreeNode _body;
        public LambdaNode(MissingLinqSql sql, LambdaExpression expression)
            : base(sql, expression)
        {
            _parameters = For(sql, expression.Parameters);
            _body = For(sql, expression.Body);
        }

        public override string Translate()
        {
            return _body.Translate();
        }
    }
}