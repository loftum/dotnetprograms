using System.Data;
using System.Data.SqlClient;
using DbTool.Lib.ExtensionMethods;

namespace DbToolGui.Data
{
    public class SchemaLoader
    {
        private const string TableName = "TABLE_NAME";
        private const string ColumnName = "COLUMN_NAME";

        private readonly SqlConnection _sqlConnection;

        public SchemaLoader(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Schema Load()
        {
            var schema = new Schema();

            try
            {
                _sqlConnection.Open();
                var schemaTable = _sqlConnection.GetSchema("Columns");

                foreach (DataRow row in schemaTable.Rows)
                {
                    var table = schema.GetOrCreateTable(row.Get<string>(TableName));
                    table.Add(new SchemaColumn(table, row.Get<string>(ColumnName)));
                }
            }
            finally
            {
                _sqlConnection.Close();
            }

            return schema;
        }
    }
}