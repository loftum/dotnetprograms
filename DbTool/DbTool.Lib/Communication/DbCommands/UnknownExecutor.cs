using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Exceptions;

namespace DbTool.Lib.Communication.DbCommands
{
    public class UnknownExecutor : IDbCommandExecutor
    {
        public IDbCommandResult Execute(string command)
        {
            return new ErrorResult(new UserException(ExceptionType.UnknownDatabaseCommand, command));
        }
    }
}