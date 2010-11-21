using System.Collections.Generic;

namespace MovieBase.Domain
{
    public class Movie : DomainObject
    {
        public virtual string Title { get; set; }
        public virtual IList<Category> Categories { get; set; }

        public Movie()
        {
            Categories = new List<Category>();
        }

        public virtual void AddCategory(Category category)
        {
            category.AddMovie(this);
            Categories.Add(category);
        }
    }
}