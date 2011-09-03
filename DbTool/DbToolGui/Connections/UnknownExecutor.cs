using DbToolGui.Exceptions;

namespace DbToolGui.Connections
{
    public class UnknownExecutor : IDbCommandExecutor
    {
        public IDbCommandResult Execute(string command)
        {
            return new ErrorResult(new UserException(ExceptionType.UnknownDatabaseCommand, command));
        }
    }
}