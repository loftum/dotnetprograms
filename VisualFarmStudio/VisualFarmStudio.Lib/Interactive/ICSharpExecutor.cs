namespace VisualFarmStudio.Lib.Interactive
{
    public interface ICSharpExecutor
    {
        CSharpResult Execute(string statement);
        string Vars { get; }
    }
}