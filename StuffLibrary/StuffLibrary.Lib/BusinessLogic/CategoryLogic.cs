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
            return _repo.GetAll<Category>();
        }

        public Category GetCategory(long id)
        {
            return _repo.Get<Category>(id);
        }

        public long Save(Category updated)
        {
            if (updated.IsNew())
            {
                _repo.Add(updated);
                _repo.SaveChanges();
                return updated.Id;
            }
            else
            {
                var existing = _repo.Get<Category>(updated.Id);
                existing.Name = updated.Name;
                _repo.SaveChanges();
                return existing.Id;
            }
        }
    }
}