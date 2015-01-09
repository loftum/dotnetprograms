using System.Configuration;

namespace MongoTool.Core.Configuration
{
    [ConfigurationCollection(typeof(AssemblyElement))]
    public class AssemblyCollection : ConfigurationElementCollection<AssemblyElement>
    {
        [ConfigurationProperty("sourceFolder", IsRequired = true)]
        public string SourceFolder
        {
            get { return (string)this["sourceFolder"]; }
            set { this["sourceFolder"] = value; }
        }

        public AssemblyCollection() : base("load")
        {
        }

        protected override object GetElementKey(AssemblyElement element)
        {
            return element.FileName;
        }
    }
}