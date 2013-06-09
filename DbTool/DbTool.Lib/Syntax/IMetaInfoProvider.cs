using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Syntax
{
    public interface IMetaInfoProvider
    {
        TagType GetTypeOf(string word);
        TableMeta GetType(string word);
    }
}