using System.Collections.Generic;
using System.Linq;

namespace DbTool.Lib.Meta.Types
{
    public class ColumnMeta : TypeMeta
    {
        public ColumnMeta(string typeName, string columnName) : base(typeName, columnName)
        {
        }

        public override IEnumerable<TypeMeta> Members
        {
            get { return Enumerable.Empty<TypeMeta>(); }
        }

        public override IEnumerable<TypeMeta> Properties
        {
            get { return Enumerable.Empty<TypeMeta>(); }
        }
    }
}