using System.Collections.Generic;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Meta.Types
{
    public class TableMeta
    {
        public string Name { get; private set; }

        public TableMeta(IDictionary<string, object> row) : this(row.Get<string>("TABLE_NAME"))
        {
        }

        public TableMeta(string name)
        {
            Name = name;
        }

        public void AddColumn(ColumnMeta meta)
        {
            _columns.Add(meta);
        }

        private readonly IList<ColumnMeta> _columns = new List<ColumnMeta>();
        public IEnumerable<ColumnMeta> Columns { get { return _columns; } }
    }
}