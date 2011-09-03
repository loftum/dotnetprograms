namespace DbToolGui.Connections
{
    public interface IDbCommandExecutor
    {
        IDbCommandResult Execute(string command);
    }
}