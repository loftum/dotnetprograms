using System;
using System.Collections.Generic;
using System.Diagnostics;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class SetSettings : CommandBase
    {
        public SetSettings(IDbToolLogger logger, IDbToolSettings settings)
            : base("set", logger, settings)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "<setting>:<value>".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return @"BackupDirectory:C:\Backup".AsArray();
        }

        public override bool AreValid(CommandArgs args)
        {
            return true;
        }

        public override void DoExecute(CommandArgs args)
        {
            if (!args.HasArguments)
            {
                ViewSettings();
                return;
            }
            TrySet(args);
        }

        private void TrySet(CommandArgs args)
        {
            var split = args.Arguments[0].Split(':');
            if (split.Length < 2)
            {
                throw new DbToolException(string.Format("Invalid parameter {0}", args.Arguments[0]));
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