namespace DbTool.Lib.Objects.Database
{
    public abstract class DatabaseObject : DbToolObject
    {
        protected DatabaseObject(string nameSpace, string name) : base(nameSpace, name)
        {
        }

        public override void AddProperty(DbToolProperty column)
        {
            var lower = column.Name.ToLowerInvariant();
            PropertyDictionary[lower] = column;
        }
    }
}