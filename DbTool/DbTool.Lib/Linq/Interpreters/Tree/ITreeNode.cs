namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public interface ITreeNode
    {
        DbToolSql Sql { get; }
        string Translate();
    }
}