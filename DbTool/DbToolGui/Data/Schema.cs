using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbToolGui.Data
{
    public class Schema
    {
        private readonly IList<SchemaTable> _tables;
        public IEnumerable<SchemaTable> Tables { get { return _tables; } }

        public Schema()
        {
            _tables = new List<SchemaTable>();
        }

        public SchemaTable GetOrCreateTable(string name)
        {
            var table = _tables.Where(t => t.Name.Equals(name)).FirstOrDefault();
            if (table == null)
            {
                table = new SchemaTable(this, name);
                _tables.Add(table);
            }
            return table;
        }

        public bool ContainsObject(string word)
        {
            return Tables.Any(table => table.Name.EqualsIgnoreCase(word) || table.ContainsObject(word));
        }
    }
}