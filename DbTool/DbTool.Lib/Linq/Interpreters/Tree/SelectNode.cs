using System.Collections.Generic;
using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class SelectNode : SqlTreeNode<MethodCallExpression>
    {
        private readonly IList<string> _columns;
        private readonly LambdaSelector _lambda;
        private readonly ITreeNode _nextNode;

        public SelectNode(DbToolSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            _lambda = new LambdaSelector(Expression.Arguments[1].GetLambda());
            _columns = _lambda.Properties;
            sql.Verb = "select";
            sql.What = string.Join(", ", _columns);
            _nextNode = For(sql, expression.Arguments[0]);
        }

        public override string Translate()
        {
            return string.Format("select {0}", string.Join(", ", _columns));
        }
    }
}