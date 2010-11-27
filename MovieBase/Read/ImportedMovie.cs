using System.Collections.Generic;

namespace Read
{
    public class ImportedMovie
    {
        public string Title { get; set; }
        private readonly ISet<string> _categories;
        public IEnumerable<string> Categories
        {
            get { return _categories; }
        }

        public ImportedMovie()
        {
            _categories = new SortedSet<string>();
        }

        public void AddCategory(string category)
        {
            _categories.Add(category);
        }
    }
}