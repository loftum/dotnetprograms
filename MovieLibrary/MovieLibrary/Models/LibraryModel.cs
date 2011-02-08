using System.Collections.Generic;

namespace MovieLibrary.Models
{
    public class LibraryModel
    {
        public IEnumerable<MovieModel> Movies { get; set; }
    }
}