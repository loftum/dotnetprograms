using System.Collections.Generic;

namespace DbTool.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Usage { get; }
        string Example { get; }

        void Execute(IList<string> args);
        string GenerateUsageText();
    }
}