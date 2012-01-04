namespace DbTool.Lib.Ui.Syntax
{
    public interface ISyntaxProvider
    {
        bool IsSqlKeyword(string word);
        bool IsCsharpKeyword(string word);
        bool IsFunction(string word);
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
    }
}