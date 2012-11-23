using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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


                var typeContainer = new TypeContainer(GetNamespaceFrom(schemaTable), schemaTable.CaseSensitive);
                var columns = GetColumns(schemaTable);
                foreach (DataRow row in schemaTable.Rows)
                {

                    var dictionary = row.ToDictionary();
                    var tableMeta = (TableMeta) typeContainer.GetOrAdd(new TableMeta(dictionary));
                    tableMeta.AddColumn(new ColumnMeta(row));
                }
                return typeContainer;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private IEnumerable<string> GetColumns(DataTable schemaTable)
        {
            return (from DataColumn column in schemaTable.Columns select column.ColumnName).ToList();
        }

        private string GetNamespaceFrom(DataTable schemaTable)
        {
            var row = schemaTable.Rows[0];
            return string.Format("{0}.{1}", row.Get<string>("TABLE_SCHEMA"), row.Get<string>("TABLE_CATALOG"));
        }
    }
}