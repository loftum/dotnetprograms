namespace StuffLibrary.Models.Grids
{
    public interface IGridRowViewModel
    {
        string RowId { get; }
        object OrderByValue(string orderBy);
        JqGridRow ToJqGridRow();
    }
}