using DbTool.Lib.Data;

namespace DbTool.Lib.Ui.Syntax
{
    public interface ISchemaObjectProvider
    {
        Schema Schema { get; set; }
        TagType GetTypeOf(string word);
    }
}