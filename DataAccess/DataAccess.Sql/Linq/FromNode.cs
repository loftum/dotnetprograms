using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq
{
    public class FromNode : SqlNode
    {
        public ISelectSource Source { get; set; }
        public string Alias { get; set; }

        public FromNode(SqlNode parent) : base(parent)
        {
            Alias = NextAlias();
        }

        public override string ToSql()
        {
            return string.Format("from {0} as {1}", Source.Sql, Alias);
        }
    }
}