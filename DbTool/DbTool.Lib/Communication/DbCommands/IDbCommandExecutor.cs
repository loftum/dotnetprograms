namespace DbTool.Lib.Communication.DbCommands
{
    public interface IDbCommandExecutor
    {
        IDbCommandResult Execute(string command);
    }
}