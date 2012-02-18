namespace DbTool.Lib.Objects.CSharp
{
    public class CSharpObject : DbToolObject
    {
        public CSharpObject(string nameSpace, string name) : base(nameSpace, name)
        {
        }

        public override void AddProperty(DbToolProperty property)
        {
            PropertyDictionary[property.Name] = property;
        }
    }
}