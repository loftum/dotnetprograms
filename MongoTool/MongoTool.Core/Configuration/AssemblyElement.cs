using System.Configuration;

namespace MongoTool.Core.Configuration
{
    public class AssemblyElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get { return (string)this["fileName"]; }
            set { this["fileName"] = value; }
        }
    }
}