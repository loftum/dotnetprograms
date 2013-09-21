using System;
using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class TakeNode : SqlTreeNode<MethodCallExpression>
    {
        private ITreeNode _nextNode;
        public TakeNode(MissingLinqSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            sql.Count = GetCountFrom((dynamic)expression.Arguments[1]);
            _nextNode = For(sql, expression.Arguments[0]);
        }

        private static int GetCountFrom(ConstantExpression constant)
        {
            return (int) constant.Value;
        }

        private static int GetCountFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Dont't know how to get count from a {0}", invalid.GetType()));
        }

        public override string Translate()
        {
            return "";
        }
    }
}