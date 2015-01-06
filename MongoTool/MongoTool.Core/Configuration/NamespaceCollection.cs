namespace MongoTool.Core.Configuration
{
    public class NamespaceCollection : ConfigurationElementCollection<UsingNamespace>
    {
        public NamespaceCollection() : base("using")
        {
        }

        protected override object GetElementKey(UsingNamespace element)
        {
            return element.Namespace;
        }
    }
}