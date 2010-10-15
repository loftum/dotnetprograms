using System.IO;
using System.Xml.Serialization;
using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.SummaryFormatting
{
    public class SummaryFormatter : ISummaryFormatter<DeploymentProfile>
    {
        public string Format(DeploymentProfile model)
        {
            return Serialize(model);
        }

        private static string Serialize(DeploymentProfile model)
        {
            string summary;
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(model.GetType());
                serializer.Serialize(stream, model);
                using (var streamReader = new StreamReader(stream))
                {
                    summary = streamReader.ReadToEnd();
                }
            }
            return summary;
        }
    }
}