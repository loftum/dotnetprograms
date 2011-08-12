using System.Collections.Generic;
using System.Web.Mvc;
using StuffLibrary.Domain;

namespace StuffLibrary.Models.Movies
{
    public class MovieViewModel : ViewModelBase
    {
        public Movie Movie { get; set; }

        public string Title
        {
            get { return Movie.IsNew() ? GenerateTitle("Register Movie") : GenerateTitle("View Movie"); }
        }

        public string MovieTitle
        {
            get { return Movie.IsNew() ? "New" : Movie.Title; }
        }

        public IEnumerable<SelectListItem> AvailableCategories { get; set; }

        public MovieViewModel() : this(new Movie())
        {
        }

        public MovieViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}