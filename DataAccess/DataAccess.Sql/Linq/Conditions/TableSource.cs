using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Linq.Conditions
{
    public class TableSource : ISelectSource
    {
        public string Sql { get { return Table.InBrackets(); } }

        public string Table { get; private set; }

        public TableSource(string table)
        {
            Table = table;
        }
    }
}