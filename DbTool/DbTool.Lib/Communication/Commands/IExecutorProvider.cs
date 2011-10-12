namespace DbTool.Lib.Communication.Commands
{
    public interface IExecutorProvider
    {
        IDbCommandExecutor GetExecutorFor(string statement);
    }
}