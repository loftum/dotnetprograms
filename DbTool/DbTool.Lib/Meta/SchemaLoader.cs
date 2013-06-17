using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
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

                var primaryKeys = GetPrimaryKeys().ToList();
                var schema = new DatabaseSchema(schemaTable);
                foreach (DataRow row in schemaTable.Rows)
                {
                    var dictionary = row.ToDictionary();
                    var tableMeta = schema.GetOrAddTable(new TableMeta(dictionary));
                    var column = new ColumnMeta(row);
                    column.IsPrimaryKey = primaryKeys.Any(k => k.TableName == tableMeta.Name && k.ColumnName == column.Name);
                    tableMeta.AddColumn(column);
                }
                return schema;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private IEnumerable<PrimaryKey> GetPrimaryKeys()
        {
            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText = @"select *
                    from information_schema.key_column_usage
                    where objectproperty(object_id(constraint_name), 'IsPrimaryKey') = 1";

                using (var reader = command.ExecuteReader())
                {
                    foreach (IDataRecord row in reader)
                    {
                        yield return new PrimaryKey(row);
                    }
                }
            }
        }
    }
}