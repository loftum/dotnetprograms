using System.Collections.Generic;

namespace StuffLibrary.Domain
{
    public class Movie : DomainObject
    {
        public virtual string Title { get; set; }
        public virtual IList<Category> Categories { get; private set; }

        public Movie()
        {
            Categories = new List<Category>();
        }

        public virtual void AddCategory(Category category)
        {
            category.Movies.Add(this);
            Categories.Add(category);
        }
    }
}