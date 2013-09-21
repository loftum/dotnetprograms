namespace MissingLinq.Sql.Interpreters.Tree
{
    public interface ITreeNode
    {
        MissingLinqSql Sql { get; }
        string Translate();
    }
}