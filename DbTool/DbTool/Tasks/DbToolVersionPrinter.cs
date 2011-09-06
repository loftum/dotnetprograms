using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Tasks
{
    public class DbToolVersionPrinter : TaskBase
    {
        public DbToolVersionPrinter(IDbToolLogger logger, IDbToolSettings settings)
            : base("-version", string.Empty, string.Empty, logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            Logger.WriteLine("42");
        }
    }
}