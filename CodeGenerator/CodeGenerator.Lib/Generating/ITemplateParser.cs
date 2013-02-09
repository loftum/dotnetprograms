namespace CodeGenerator.Lib.Generating
{
    public interface ITemplateParser
    {
        string Parse(string template, Record record);
    }
}