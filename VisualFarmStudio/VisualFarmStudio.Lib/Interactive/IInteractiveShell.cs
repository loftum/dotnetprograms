namespace VisualFarmStudio.Lib.Interactive
{
    public interface IInteractiveShell
    {
        CSharpResult Execute(string statement);
    }
}