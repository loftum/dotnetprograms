using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public interface ITypeCache
    {
        DatabaseSchema Schema { get; set; }

        TableMeta GetType(string name);
    }
}