namespace MasterData.Migrations.ExtensionMethods
{
    public class ForeignKey
    {
        public static string ColumnTo(string table)
        {
            return string.Format("{0}_Id", table);
        }
    }
}