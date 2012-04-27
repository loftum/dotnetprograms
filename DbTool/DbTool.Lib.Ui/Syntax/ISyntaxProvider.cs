using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Ui.Syntax
{
    public interface ISyntaxProvider
    {
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
        TypeMeta GetType(string word);
        bool IsPropertyIndicator(char c);
    }
}