namespace MissingLinq.Sql
{
    public class MissingLinqSqlParameter
    {
        public int Number { get; private set; }
        public string Name { get { return string.Format("@p{0}", Number); } }
        public object Value { get; set; }

        public MissingLinqSqlParameter(int number)
        {
            Number = number;
        }
    }
}