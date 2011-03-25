using System.Collections.Generic;

namespace DbTool.Lib.Tasks
{
    public interface ITask
    {
        string Name { get; }
        string Usage { get; }
        string Example { get; }

        void Execute(IList<string> args);
        string GenerateUsageText();
    }
}