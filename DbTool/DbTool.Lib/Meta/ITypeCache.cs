using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public interface ITypeCache
    {
        TypeContainer Schema { get; set; }
        TypeMeta GetType(string name);
    }
}