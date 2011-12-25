using System.IO;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using Newtonsoft.Json;

namespace DbTool.Lib.Serializing
{
    public class DbToolSettingsSerializer
    {
        public static DbToolSettings From(string path)
        {
            var serialized = Read(path);
            var settings = JsonConvert.DeserializeObject<DbToolSettings>(serialized);
            settings.Contexts.Each(context =>
                context.Databases
                    .Each(connection => connection.Parent = context));
            return settings;
        }

        private static string Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new DbToolException("Missing settings file: " + path);
            }
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Save(DbToolSettings settings, string path)
        {
            using (var writer = new StreamWriter(File.Create(path)))
            {
                var serialized = Serialize(settings);
                writer.Write(serialized);
                writer.Flush();
                writer.Close();
            }
        }

        private static string Serialize(DbToolSettings settings)
        {
            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }
    }
}