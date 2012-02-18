namespace DbTool.Lib.Objects
{
    public class DbToolProperty
    {
        public string Name { get; private set; }

        public DbToolProperty(string name)
        {
            Name = name;
        }
    }
}