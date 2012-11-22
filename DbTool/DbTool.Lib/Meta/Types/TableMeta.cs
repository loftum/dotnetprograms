using System.Collections.Generic;
using System.Linq;

namespace DbTool.Lib.Meta.Types
{
    public class TableMeta : TypeMeta
    {
        private readonly IList<ColumnMeta> _columns = new List<ColumnMeta>();

        public TableMeta(string tableName) : base(tableName, tableName)
        {
        }

        public void AddColumn(ColumnMeta meta)
        {
            _columns.Add(meta);
        }

        public override IEnumerable<TypeMeta> Members
        {
            get { return _columns; }
        }

        public override IEnumerable<TypeMeta> Properties
        {
            get { return _columns; }
        }

        public IEnumerable<ColumnMeta> Columns
        {
            get { return Properties.Cast<ColumnMeta>(); }
        }
    }
}