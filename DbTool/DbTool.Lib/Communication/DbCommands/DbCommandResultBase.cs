namespace DbTool.Lib.Communication.DbCommands
{
    public abstract class DbCommandResultBase : IDbCommandResult
    {
        public override string ToString()
        {
            return ConvertToString();
        }

        protected abstract string ConvertToString();
    }
}