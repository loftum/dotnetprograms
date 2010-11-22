using System.Linq;
using MovieBase.Comparison.TextComparison;
using MovieBase.Data.Dao;
using MovieBase.Domain;
using MovieBase.Events;
using MovieBase.Views;

namespace MovieBase.Controllers
{
    public class MovieBaseController : IMovieBaseController
    {
        private readonly IMovieBaseView _view;
        private readonly IMovieBaseRepository _repository;

        public MovieBaseController(IMovieBaseRepository repository, IMovieBaseView view)
        {
            _view = view;
            _view.Search += Search;
            _repository = repository;
        }

        private void Search(object sender, SearchEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.SearchText))
            {
                return;
            }
            var movies = _repository.GetAll<Movie>().Where(movie => StringValue.Of(movie.Title).Like(args.SearchText));

        }
    }
}