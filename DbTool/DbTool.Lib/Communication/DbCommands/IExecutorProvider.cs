namespace DbTool.Lib.Communication.DbCommands
{
    public interface IExecutorProvider
    {
        IDbCommandExecutor GetExecutorFor(string statement);
    }
}