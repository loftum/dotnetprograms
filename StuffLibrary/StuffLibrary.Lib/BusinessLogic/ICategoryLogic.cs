using System.Collections.Generic;
using StuffLibrary.Domain;

namespace StuffLibrary.Lib.BusinessLogic
{
    public interface ICategoryLogic
    {
        IEnumerable<Category> GetCategories();
    }
}