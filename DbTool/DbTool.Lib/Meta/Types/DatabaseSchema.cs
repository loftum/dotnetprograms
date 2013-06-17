using System.Collections.Generic;
using System.Data;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class DatabaseSchema : IDatabaseSchema
    {
        public bool CaseSensitive { get; private set; }
        public string TableSchema { get; private set; }
        public string TableCatalog { get; private set; }
        public string FullName { get { return string.Format("{0}.{1}", TableSchema, TableCatalog); } }

        private readonly IDictionary<string, TableMeta> _tables = new Dictionary<string, TableMeta>();

        public DatabaseSchema(DataTable schemaTable)
        {
            var row = schemaTable.Rows[0];
            TableSchema = row.Get<string>("TABLE_SCHEMA");
            TableCatalog = row.Get<string>("TABLE_CATALOG");
            CaseSensitive = schemaTable.CaseSensitive;
        }

        public IEnumerable<TableMeta> Tables
        {
            get { return _tables.Values; }
        }

        public TableMeta GetTable(string name)
        {
            var typeName = CaseSensitive ? name : name.ToLowerInvariant();
            return _tables.ContainsKey(typeName) ? _tables[typeName] : null;
        }

        public TableMeta AddTable(TableMeta table)
        {
            var typeName = CaseSensitive ? table.Name : table.Name.ToLowerInvariant();
            _tables[typeName] = table;
            return table;
        }

        public TableMeta GetOrAddTable(TableMeta table)
        {
            var existing = GetTable(table.Name);
            return existing ?? AddTable(table);
        }
    }
}