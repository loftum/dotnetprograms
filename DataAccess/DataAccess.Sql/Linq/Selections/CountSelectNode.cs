using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq.Selections
{
    public class CountSelectNode : SelectNode
    {
        public CountSelectNode(SqlNode parent) : base(parent)
        {
        }

        public override string ToSql()
        {
            return string.Format("count('x')");
        }
    }
}