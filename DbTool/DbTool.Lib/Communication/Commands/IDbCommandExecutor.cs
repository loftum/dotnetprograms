namespace DbTool.Lib.Communication.Commands
{
    public interface IDbCommandExecutor
    {
        IDbCommandResult Execute(string command);
    }
}