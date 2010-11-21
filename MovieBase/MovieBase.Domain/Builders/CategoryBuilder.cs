namespace MovieBase.Domain.Builders
{
    public class CategoryBuilder : BuilderBase<Category>
    {
        private CategoryBuilder(Category item) : base(item)
        {
        }

        public static CategoryBuilder Category()
        {
            var category = new Category();
            return new CategoryBuilder(category);
        }

        public CategoryBuilder WithName(string name)
        {
            Item.Name = name;
            return this;
        }
    }
}