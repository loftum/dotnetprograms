using System.Linq.Expressions;
using System.Reflection;

namespace DataAccess.Sql.Linq.Conditions
{
    public class ColumnSelector : SqlNode
    {
        public string Alias { get; private set; }
        public SqlNode Argument { get; private set; }

        public ColumnSelector(MemberInfo member, Expression argument, SqlNode parent) : base(parent)
        {
            Alias = member.Name;
            Argument = For(argument);
        }

        public override string ToSql()
        {
            return string.Format("{0} as {1}", Argument, Alias);
        }
    }
}