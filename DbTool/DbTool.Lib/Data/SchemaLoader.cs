using System.Data;
using System.Data.Common;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Objects;
using DbTool.Lib.Objects.Database;

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

        public SchemaObjectContainer Load()
        {
            try
            {
                _dbConnection.Open();
                var schemaTable = _dbConnection.GetSchema("Columns");
                var container = new SchemaObjectContainer();

                var schema = container.GetOrCreateNameSpace(schemaTable.Namespace);
                foreach (DataRow row in schemaTable.Rows)
                {
                    var table = schema.GetOrCreateTable(row.Get<string>(TableName));
                    table.AddProperty(new DbToolProperty(row.Get<string>(ColumnName)));
                }
                return container;
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}