namespace DbTool.Lib.Ui.Syntax
{
    public interface ISyntaxProvider
    {
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
    }
}