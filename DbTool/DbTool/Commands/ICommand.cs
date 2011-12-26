using System.Collections.Generic;

namespace DbTool.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(IList<string> args);
        string GenerateUsageText();
    }
}