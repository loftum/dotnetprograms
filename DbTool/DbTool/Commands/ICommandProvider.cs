using System.Collections.Generic;

namespace DbTool.Commands
{
    public interface ICommandProvider
    {
        ICommand GetCommand(IList<string> args);
    }
}