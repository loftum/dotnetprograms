using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class ViewDbToolVersion : CommandBase
    {
        public ViewDbToolVersion(IDbToolLogger logger, IDbToolSettings settings)
            : base("--version", string.Empty, string.Empty, logger, settings)
        {
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