using System.Collections.Generic;
using System.Linq;

namespace DbTool.Lib.Meta.Types
{
    public class NullTypeMeta : TypeMeta
    {
        public NullTypeMeta() :base("null", "null")
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