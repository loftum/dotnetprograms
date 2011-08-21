using StuffLibrary.Domain;

namespace StuffLibrary.Models.Categories
{
    public class CategoryViewModel
    {
        public Category Category { get; private set; }

        public string CategoryName
        {
            get { return Category.IsNew() ? "New" : Category.Name; }
        }

        public CategoryViewModel() : this(new Category())
        {
            
        }

        public CategoryViewModel(Category category)
        {
            Category = category;
        }
    }
}