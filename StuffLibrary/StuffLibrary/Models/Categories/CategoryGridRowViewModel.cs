using StuffLibrary.Attributes;
using StuffLibrary.Domain;
using StuffLibrary.Models.Grids;

namespace StuffLibrary.Models.Categories
{
    public class CategoryGridRowViewModel : IGridRowViewModel
    {
        public string RowId { get { return Id.ToString(); } }
        [TransferToGrid]
        public long Id { get; set; }
        [TransferToGrid]
        public string Name { get; set; }

        public CategoryGridRowViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}