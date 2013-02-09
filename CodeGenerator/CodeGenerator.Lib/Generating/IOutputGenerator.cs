namespace CodeGenerator.Lib.Generating
{
    public interface IOutputGenerator
    {
        string Generate(string input, string template, int linesPerRecord, string delimiter);
    }
}