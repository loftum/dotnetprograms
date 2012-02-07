namespace DbTool.Lib.CSharp
{
    public interface ICSharpEvaluator
    {
        void Init();
        CSharpResult Run(string code);
    }
}