namespace DbTool.Lib.Linq
{
    public class OrderColumn
    {
        public string Name { get; private set; }
        public bool Ascending { get; private set; }

        public string Statement
        {
            get { return Ascending ? Name : string.Format("{0} desc", Name); }
        }

        public OrderColumn(string name, bool ascending)
        {
            Name = name;
            Ascending = ascending;
        }

        public override string ToString()
        {
            return Statement;
        }
    }
}