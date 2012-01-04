using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Data
{
    public class SchemaTable
    {
        public Schema Schema { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<SchemaColumn> Columns
        {
            get { return _columns.Values; }
        }

        private readonly IDictionary<string, SchemaColumn> _columns;

        public SchemaTable(Schema schema, string name)
        {
            Schema = schema;
            Name = name;
            _columns = new Dictionary<string, SchemaColumn>();
        }

        public void Add(SchemaColumn column)
        {
            var lower = column.Name.ToLowerInvariant();
            _columns[lower] = column;
        }

        public bool ContainsObject(string word)
        {
            var lower = word.ToLowerInvariant();
            return _columns.ContainsKey(word) || _columns.Values.Any(c => c.Name.EqualsIgnoreCase(lower));
        }
    }
}