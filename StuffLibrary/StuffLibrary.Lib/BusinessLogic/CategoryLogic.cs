using System.Collections.Generic;
using StuffLibrary.Domain;
using StuffLibrary.Repository;

namespace StuffLibrary.Lib.BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly IStuffLibraryRepo _repo;

        public CategoryLogic(IStuffLibraryRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _repo.GetAll<Category>().List();
        }
    }
}