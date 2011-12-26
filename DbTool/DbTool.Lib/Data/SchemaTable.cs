using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Data
{
    public class SchemaTable
    {
        public Schema Schema { get; private set; }
        public string Name { get; private set; }
        private readonly IList<SchemaColumn> _columns;
        public IEnumerable<SchemaColumn> Columns { get { return _columns; } }

        public SchemaTable(Schema schema, string name)
        {
            Schema = schema;
            Name = name;
            _columns = new List<SchemaColumn>();
        }

        public void Add(SchemaColumn column)
        {
            _columns.Add(column);
        }

        public bool ContainsObject(string word)
        {
            return Columns.Any(c => c.Name.EqualsIgnoreCase(word));
        }
    }
}