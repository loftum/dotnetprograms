namespace DbTool.Lib.Linq
{
    public class DbToolSqlParameter
    {
        public int Number { get; private set; }
        public string Name { get { return string.Format("@p{0}", Number); } }
        public object Value { get; set; }

        public DbToolSqlParameter(int number)
        {
            Number = number;
        }
    }
}