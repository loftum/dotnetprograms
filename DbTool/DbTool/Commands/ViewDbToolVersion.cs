using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class ViewDbToolVersion : CommandBase
    {
        public ViewDbToolVersion(IDbToolLogger logger, IDbToolSettings settings)
            : base("--version", logger, settings)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return new string[0];
        }

        protected override IEnumerable<string> GetExamples()
        {
            return new string[0];
        }

        public override bool AreValid(CommandArgs args)
        {
            return true;
        }

        public override void DoExecute(CommandArgs args)
        {
            Logger.WriteLine(DbToolValues.Version);
        }
    }
}