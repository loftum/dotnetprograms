using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class MemberNode : SqlTreeNode<MemberExpression>
    {
        private readonly string _memberName;
        public MemberNode(DbToolSql sql, MemberExpression expression) : base(sql, expression)
        {
            _memberName = Expression.Member.Name;
        }

        public override string Translate()
        {
            return _memberName;
        }
    }
}