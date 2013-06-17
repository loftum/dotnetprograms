using System.Collections.Generic;

namespace DbTool.Lib.Meta.Types
{
    public interface IDatabaseSchema
    {
        bool CaseSensitive { get; }
        string TableSchema { get; }
        string TableCatalog { get; }
        string FullName { get; }
        IEnumerable<TableMeta> Tables { get; }
        TableMeta GetTable(string name);
    }
}