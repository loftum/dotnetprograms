using System.Collections.Generic;

namespace DbTool.Lib.Data
{
    public class Schema
    {
        private readonly ISet<string> _columnNames; 
        private readonly ISet<string> _tableNames;
        private readonly IDictionary<string, SchemaTable> _tables;

        public Schema()
        {
            _tables = new Dictionary<string, SchemaTable>();
            _tableNames = new HashSet<string>();
            _columnNames = new HashSet<string>();
        }

        public SchemaTable GetOrCreateTable(string name)
        {
            var lower = name.ToLowerInvariant();
            var table = GetTable(lower);
            if (table == null)
            {
                table = new SchemaTable(this, name);
                _tables[lower] = table;
            }
            return table;
        }

        private SchemaTable GetTable(string lowerCaseName)
        {
            try
            {
                return _tables[lowerCaseName];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public bool ContainsColumn(string lowerCase)
        {
            return _columnNames.Contains(lowerCase);
        }

        public bool ContainsTable(string lowerCase)
        {
            return _tableNames.Contains(lowerCase);
        }

        public void RefreshObjectNameCache()
        {
            _tableNames.Clear();
            _columnNames.Clear();
            foreach (var table in _tables.Values)
            {
                _tableNames.Add(table.Name.ToLowerInvariant());
                foreach (var column in table.Columns)
                {
                    _columnNames.Add(column.Name.ToLowerInvariant());
                }
            }
        }
    }
}