using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class MemberNode : SqlTreeNode<MemberExpression>
    {
        private readonly string _memberName;
        public MemberNode(MissingLinqSql sql, MemberExpression expression) : base(sql, expression)
        {
            _memberName = Expression.Member.Name;
        }

        public override string Translate()
        {
            return _memberName;
        }
    }
}