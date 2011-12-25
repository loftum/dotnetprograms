namespace DbTool.Lib.Collections
{
    public interface IChild<TParent>
    {
        TParent Parent { get; set; }
    }
}