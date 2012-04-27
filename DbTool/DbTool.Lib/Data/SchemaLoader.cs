using System.Data;
using System.Data.Common;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Data
{
    public class SchemaLoader
    {
        private const string TableName = "TABLE_NAME";
        private const string ColumnName = "COLUMN_NAME";
        private const string ColumnType = "DATA_TYPE";

        private readonly DbConnection _dbConnection;

        public SchemaLoader(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public TypeContainer Load()
        {
            try
            {
                _dbConnection.Open();
                var schemaTable = _dbConnection.GetSchema("Columns");
                var typeContainer = new TypeContainer(schemaTable.Namespace);

                foreach (DataRow row in schemaTable.Rows)
                {
                    var tableMeta = (TableMeta) typeContainer.GetOrAdd(new TableMeta(row.Get<string>(TableName)));
                    tableMeta.AddColumn(new ColumnMeta(row.Get<string>(ColumnType), row.Get<string>(ColumnName)));
                }
                return typeContainer;
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}