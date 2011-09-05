namespace DbToolGui.Communication.Commands
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
            return string.Format("{0} number of rows affected.", AffectedRows);
        }
    }
}