namespace DbTool.Lib.Communication.DbCommands.Results
{
    public class MigrationResult : DbCommandResultBase
    {
        public string Text { get; private set; }

        public MigrationResult(string text)
        {
            Text = text;
        }

        protected override string ConvertToString()
        {
            return Text;
        }
    }
}