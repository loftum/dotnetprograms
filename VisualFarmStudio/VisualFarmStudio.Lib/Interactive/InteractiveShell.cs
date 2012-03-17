namespace VisualFarmStudio.Lib.Interactive
{
    public class InteractiveShell : IInteractiveShell
    {
        private readonly ICSharpExecutor _executor;

        public InteractiveShell(ICSharpExecutor executor)
        {
            _executor = executor;
            
        }

        public CSharpResult Execute(string statement)
        {
            return _executor.Execute(statement);
        }
    }
}