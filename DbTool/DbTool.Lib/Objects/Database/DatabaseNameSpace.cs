namespace DbTool.Lib.Objects.Database
{
    public class DatabaseNameSpace : CaseInsensitiveNameSpace
    {
        public DatabaseNameSpace(string name) : base(name)
        {
        }

        public DbToolObject GetOrCreateTable(string name)
        {
            var table = Get(name);
            if (table == null)
            {
                table = new TableObject(Name, name);
                Add(table);
            }
            return table;
        }
    }
}