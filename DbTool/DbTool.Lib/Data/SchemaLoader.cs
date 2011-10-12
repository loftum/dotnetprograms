using System.Data;
using System.Data.Common;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Data
{
    public class SchemaLoader
    {
        private const string TableName = "TABLE_NAME";
        private const string ColumnName = "COLUMN_NAME";

        private readonly DbConnection _dbConnection;

        public SchemaLoader(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Schema Load()
        {
            var schema = new Schema();

            try
            {
                _dbConnection.Open();
                var schemaTable = _dbConnection.GetSchema("Columns");

                foreach (DataRow row in schemaTable.Rows)
                {
                    var table = schema.GetOrCreateTable(row.Get<string>(TableName));
                    table.Add(new SchemaColumn(table, row.Get<string>(ColumnName)));
                }
            }
            finally
            {
                _dbConnection.Close();
            }

            return schema;
        }
    }
}