using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Ui.Syntax
{
    public interface IMetaInfoProvider
    {
        TagType GetTypeOf(string word);
        TypeMeta GetType(string word);
    }
}