using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbTool.Lib.Data;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.Results
{
    public class QueryResult : DbCommandResultBase
    {
        private const string RowNum = "#";

        public string Query { get; set; }
        public long Rowcount { get; private set; }

        private readonly IList<ColumnDescriptor> _columns;
        public IEnumerable<ColumnDescriptor> Columns { get { return _columns; } }


        private readonly IList<Record> _records;
        public IEnumerable<Record> Rows
        {
            get { return _records; }
        }

        public QueryResult()
        {
            _columns = new List<ColumnDescriptor>();
            _records = new List<Record>();
            AddColumn(RowNum, typeof(long));
        }

        public void AddRow(IEnumerable values)
        {
            var list = new ArrayList();
            list.Add(++Rowcount);
            foreach (var value in values)
            {
                list.Add(value);
            }
            
            var record = new Record();
            for (var ii = 0; ii < _columns.Count; ii++)
            {
                record.Add(new Property(_columns[ii].Name, list[ii]));
            }
            _records.Add(record);
        }

        public void AddColumn(string name, Type type)
        {
            if (name.IsNullOrEmpty())
            {
                name = "(Unknown)";
            }
            _columns.Add(new ColumnDescriptor(NextIndex(), name, type));
        }

        private int NextIndex()
        {
            return _columns.Count;
        }

        protected override string ConvertToString()
        {
            var builder = new StringBuilder();
            var columnNames = _columns.Select(c => c.Name);
            builder.AppendLine(string.Join(" | ", columnNames));
            foreach (var row in Rows)
            {
                var values = columnNames.Select(columnName => row.Get(columnName));
                builder.AppendLine(string.Join(" | ", values));
            }
            return builder.ToString();
        }
    }
}