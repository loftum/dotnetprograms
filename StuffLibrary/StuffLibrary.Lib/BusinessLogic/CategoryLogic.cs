using System.Collections.Generic;
using StuffLibrary.Domain;
using StuffLibrary.Lib.UnitOfWork;
using StuffLibrary.Repository;

namespace StuffLibrary.Lib.BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly IStuffLibraryRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryLogic(IStuffLibraryRepo repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
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
            using (var work = _unitOfWork.Begin())
            {
                if (updated.IsNew())
                {
                    _repo.Add(updated);
                    work.Complete();
                    return updated.Id;
                }
                
                var existing = _repo.Get<Category>(updated.Id);
                existing.Name = updated.Name;
                work.Complete();
                return existing.Id;
            }
        }
    }
}