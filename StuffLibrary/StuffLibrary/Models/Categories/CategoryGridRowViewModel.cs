using StuffLibrary.Attributes;
using StuffLibrary.Domain;
using StuffLibrary.Models.Grids;

namespace StuffLibrary.Models.Categories
{
    public class CategoryGridRowViewModel : GridRowViewModelBase<Category>
    {
        [TransferToGrid]
        public long Id { get; set; }
        [TransferToGrid]
        public string Name { get; set; }

        public CategoryGridRowViewModel(Category category) : base(category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public override object OrderByValue(string orderBy)
        {
            switch(orderBy)
            {
                case "Name":
                    return Name;
                default:
                    return string.Empty;
            }
        }
    }
}