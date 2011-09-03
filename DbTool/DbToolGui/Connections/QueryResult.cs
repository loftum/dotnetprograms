using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.Connections
{
    public class QueryResult : DbCommandResultBase
    {
        public string Query { get; set; }

        private readonly IList<ColumnDescriptor> _columns;
        public IEnumerable<ColumnDescriptor> Columns { get { return _columns; } }

        private readonly IList<IDictionary<string, object>> _rows;
        public IEnumerable<IDictionary<string, object>> Rows { get { return _rows; } }

        public QueryResult()
        {
            _columns = new List<ColumnDescriptor>();
            _rows = new List<IDictionary<string, object>>();
        }

        public void AddRow(IEnumerable<object> values)
        {
            var list = values.ToList();
            var row = new Dictionary<string, object>();
            for (var ii=0; ii<_columns.Count; ii++)
            {
                row[_columns[ii].Name] = list[ii];
            }
            _rows.Add(row);
        }

        public void AddColumn(string name, Type type)
        {
            if (name.IsNullOrEmpty())
            {
                name = "(Unknown)";
            }
            _columns.Add(new ColumnDescriptor(name, type));
        }

        protected override string ConvertToString()
        {
            var builder = new StringBuilder();
            var columnNames = _columns.Select(c => c.Name);
            builder.AppendLine(string.Join(" | ", columnNames));
            foreach (var row in _rows)
            {
                var values = columnNames.Select(columnName => row[columnName]);
                builder.AppendLine(string.Join(" | ", values));
            }
            return builder.ToString();
        }
    }
}