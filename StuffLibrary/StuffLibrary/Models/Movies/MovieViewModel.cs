using StuffLibrary.Domain;
using StuffLibrary.Domain.ExtensionMethods;

namespace StuffLibrary.Models.Movies
{
    public class MovieViewModel : ViewModelBase
    {
        public Movie Movie { get; set; }

        public string Title
        {
            get { return Movie.IsNew() ? GenerateTitle("Register Movie") : GenerateTitle("View Movie"); }
        }

        public MovieViewModel() : this(new Movie())
        {
        }

        public MovieViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}