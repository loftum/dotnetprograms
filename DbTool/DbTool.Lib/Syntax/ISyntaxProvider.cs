using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Syntax
{
    public interface ISyntaxProvider
    {
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
        TypeMeta GetType(string word);
        bool IsPropertyIndicator(char c);
    }
}