using System.Data;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class PrimaryKey
    {
        public string ConstraintName { get; private set; }
        public string TableName { get; private set; }
        public string ColumnName { get; private set; }

        public PrimaryKey(IDataRecord row)
        {
            
            ConstraintName = row.Get<string>("CONSTRAINT_NAME");
            TableName = row.Get<string>("TABLE_NAME");
            ColumnName = row.Get<string>("COLUMN_NAME");
        }
    }
}