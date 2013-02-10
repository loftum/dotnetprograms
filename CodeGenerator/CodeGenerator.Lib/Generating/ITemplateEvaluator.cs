namespace CodeGenerator.Lib.Generating
{
    public interface ITemplateEvaluator
    {
        string Evaluate(string template, Record record);
    }
}