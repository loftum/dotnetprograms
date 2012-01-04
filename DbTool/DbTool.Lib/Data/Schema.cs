using System.Collections.Generic;

namespace DbTool.Lib.Data
{
    public class Schema
    {
        private readonly ISet<string> _objectNames;
        private readonly IDictionary<string, SchemaTable> _tables;

        public Schema()
        {
            _tables = new Dictionary<string, SchemaTable>();
            _objectNames = new HashSet<string>();
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

        public bool ContainsObject(string lowerCase)
        {
            return _objectNames.Contains(lowerCase);
        }

        public void RefreshObjectNameCache()
        {
            _objectNames.Clear();
            foreach (var table in _tables.Values)
            {
                _objectNames.Add(table.Name.ToLowerInvariant());
                foreach (var column in table.Columns)
                {
                    _objectNames.Add(column.Name.ToLowerInvariant());
                }
            }
        }
    }
}