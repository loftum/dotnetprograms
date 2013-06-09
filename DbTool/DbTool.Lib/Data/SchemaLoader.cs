using System.Data;
using System.Data.Common;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Data
{
    public class SchemaLoader
    {
        private readonly DbConnection _dbConnection;

        public SchemaLoader(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public DatabaseSchema Load()
        {
            try
            {
                _dbConnection.Open();
                var schemaTable = _dbConnection.GetSchema("Columns");

                var schema = new DatabaseSchema(schemaTable);
                foreach (DataRow row in schemaTable.Rows)
                {
                    var dictionary = row.ToDictionary();
                    var tableMeta = schema.GetOrAddTable(new TableMeta(dictionary));
                    tableMeta.AddColumn(new ColumnMeta(row));
                }
                return schema;
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}