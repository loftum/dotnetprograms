using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public class DbToolTypeCache : ITypeCache
    {
        public TypeContainer Schema { get; set; }

        public TypeMeta GetType(string name)
        {
            return Schema == null ? null : Schema.Get(name);
        }
    }
}