using System.Collections.Generic;

namespace StuffLibrary.Domain
{
    public class Category : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual IList<Movie> Movies { get; private set; }

        public Category()
        {
            Movies = new List<Movie>();
        }
    }
}