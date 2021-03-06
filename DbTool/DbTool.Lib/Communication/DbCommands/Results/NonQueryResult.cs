namespace DbTool.Lib.Communication.DbCommands.Results
{
    public class NonQueryResult : DbCommandResultBase
    {
        public int AffectedRows { get; private set; }

        public NonQueryResult(int affectedRows)
        {
            AffectedRows = affectedRows;
        }

        protected override string ConvertToString()
        {
            return string.Format("{0} rows affected.", AffectedRows);
        }
    }
}