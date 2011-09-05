namespace DbToolGui.Communication.Commands
{
    public interface IDbCommandExecutor
    {
        IDbCommandResult Execute(string command);
    }
}