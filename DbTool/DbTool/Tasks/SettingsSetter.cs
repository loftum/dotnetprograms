using System;
using System.Collections.Generic;
using System.Diagnostics;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;

namespace DbTool.Tasks
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

            try
            {
                Settings.Set(key, value);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Could not set {0}: {1}", key, ex.Message);
                Logger.WriteLine("Try {0} {1} to view settings", Process.GetCurrentProcess().ProcessName, Name);    
            }
        }

        private void ViewSettings()
        {
            Logger.WriteLine(Settings);
        }
    }
}