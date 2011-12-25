using System.Collections.Generic;

namespace DbTool.Lib.Tasks
{
    public interface IPopulateContextTask
    {
        void PopulateAll(bool overwriteExisting);
        void Populate(IEnumerable<string> databases, bool overwriteExisting);
    }
}