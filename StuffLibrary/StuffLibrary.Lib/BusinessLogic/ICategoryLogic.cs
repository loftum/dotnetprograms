using System.Collections.Generic;
using StuffLibrary.Domain;

namespace StuffLibrary.Lib.BusinessLogic
{
    public interface ICategoryLogic
    {
        IEnumerable<Category> GetCategories();
        long Save(Category updated);
        Category GetCategory(long id);
    }
}