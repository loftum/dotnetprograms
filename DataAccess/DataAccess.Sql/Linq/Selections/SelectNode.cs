using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq.Selections
{
    public abstract class SelectNode : SqlNode
    {
        protected SelectNode(SqlNode parent) : base(parent)
        {
        }
    }
}