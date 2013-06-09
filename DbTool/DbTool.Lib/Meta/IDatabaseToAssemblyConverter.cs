using DbTool.Lib.Meta.Emit;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public interface IDatabaseToAssemblyConverter
    {
        DynamicAssembly CreateFor(DatabaseSchema schema);
    }
}