using DbTool.Lib.Communication.DbCommands.Results;

namespace DbTool.Lib.Communication.DbCommands
{
    public interface IDbCommandExecutor
    {
        IDbCommandResult Execute(string command);
    }
}