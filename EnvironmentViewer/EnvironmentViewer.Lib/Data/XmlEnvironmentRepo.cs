using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Environment = EnvironmentViewer.Lib.Domain.Environment;

namespace EnvironmentViewer.Lib.Data
{
    public class XmlEnvironmentRepo : IEnvironmentRepo
    {
        private readonly string _filePath;

        public XmlEnvironmentRepo(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Environment> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new Environment[0];
            }
            using (var stream = File.OpenRead(_filePath))
            {
                var serializer = new XmlSerializer(typeof(EnvironmentContainer));
                var container =  (EnvironmentContainer) serializer.Deserialize(stream);
                return container.Environments;
            }
        }

        public void SaveAll(IEnumerable<Environment> environments)
        {
            var container = new EnvironmentContainer{Environments = environments.ToList()};
            var serializer = new XmlSerializer(typeof (EnvironmentContainer));
            using (var stream = File.Create(_filePath))
            {
                serializer.Serialize(stream, container);
            }
        }
    }
}