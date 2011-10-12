namespace DbTool.Lib.Data
{
    public class SchemaColumn
    {
        public SchemaTable Table { get; private set; }
        public string Name { get; private set; }

        public SchemaColumn(SchemaTable table, string name)
        {
            Table = table;
            Name = name;
            Table.Add(this);
        }
    }
}