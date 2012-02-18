using DbTool.Lib.Objects;

namespace DbTool.Lib.Ui.Syntax
{
    public interface IMetaInfoProvider
    {
        TagType GetTypeOf(string word);
        DbToolObject GetObject(string word);
    }
}