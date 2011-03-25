using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Extensions;
using DbTool.Lib.Logging;

namespace DbTool.Lib.Tasks
{
    public class SettingsSetter : TaskBase
    {
        public SettingsSetter(IDbToolLogger logger, IDbToolSettings settings)
            : base("set", "<setting>:<value>", @"BackupDirectory:C:\Backup", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            if (args.Count < 2)
            {
                ViewSettings();
                return;
            }
            TrySet(args);
        }

        private void TrySet(IList<string> args)
        {
            var split = args[1].Split(':');
            if (split.Length < 2)
            {
                throw new DbToolException(string.Format("Invalid parameter {0}", args[1]));
            }
            var key = split[0];
            var value = split[1];

            foreach(var property in Settings.GetType().GetProperties())
            {
                if (property.Name.EqualsIgnoreCase(key))
                {
                    if (CanSet(property))
                    {
                        property.SetValue(Settings, value, new object[0]);    
                    }
                    else
                    {
                        Logger.WriteLine("Cannot set {0}", property.Name);
                    }
                    return;
                }
            }
            Logger.WriteLine("Invalid setting {0}.", key);
            Logger.WriteLine("Try {0} {1} to view settings", Process.GetCurrentProcess().ProcessName, Name);
        }

        private static bool CanSet(PropertyInfo property)
        {
            return property.PropertyType.IsOneOf(typeof (string), typeof (int), typeof (long), typeof (double));
        }

        private void ViewSettings()
        {
            Logger.WriteLine(Settings);
        }
    }
}