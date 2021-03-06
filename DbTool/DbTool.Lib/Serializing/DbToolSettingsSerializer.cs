﻿using System.IO;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DotNetPrograms.Common.ExtensionMethods;
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
                context.Databases.Each(d => d.Parent = context));
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

        public static string Serialize(DbToolSettings settings)
        {
            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }
    }
}