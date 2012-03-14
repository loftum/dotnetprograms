using DbTool.Lib.Objects;

namespace DbTool.Lib.Ui.Syntax
{
    public interface ISyntaxProvider
    {
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
        DbToolObject GetObject(string word);
        bool IsPropertyIndicator(char c);
    }
}