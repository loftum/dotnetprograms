using DbTool.Lib.Data;

namespace DbToolGui.Highlighting
{
    public interface ISchemaObjectProvider
    {
        Schema Schema { get; set; }
        bool IsObject(string word);
    }
}