using DbTool.Lib.Exceptions;

namespace DbTool.Lib.Communication.Commands
{
    public class UnknownExecutor : IDbCommandExecutor
    {
        public IDbCommandResult Execute(string command)
        {
            return new ErrorResult(new UserException(ExceptionType.UnknownDatabaseCommand, command));
        }
    }
}