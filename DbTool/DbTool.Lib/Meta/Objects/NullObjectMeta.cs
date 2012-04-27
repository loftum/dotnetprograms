using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta.Objects
{
    public class NullObjectMeta : ObjectMeta
    {
        public NullObjectMeta() : base(TypeMeta.Null, null, "null")
        {
        }

        public override IEnumerable<ObjectMeta> Members
        {
            get { return Enumerable.Empty<ObjectMeta>(); }
        }

        public override IEnumerable<ObjectMeta> Properties
        {
            get { return Enumerable.Empty<ObjectMeta>(); }
        }

        public override ObjectMeta GetMember(string name)
        {
            return null;
        }
    }
}