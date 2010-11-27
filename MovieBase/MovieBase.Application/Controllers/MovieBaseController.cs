using MovieBase.AppLib.Events;
using MovieBase.AppLib.Views;
using MovieBase.Data.Services;

namespace MovieBase.AppLib.Controllers
{
    public class MovieBaseController : IMovieBaseController
    {
        private readonly IMovieBaseView _view;
        private readonly IMovieBaseService _movieBaseService;

        public MovieBaseController(IMovieBaseService movieBaseService, IMovieBaseView view)
        {
            _view = view;
            _view.Search += Search;
            _movieBaseService = movieBaseService;
        }

        private void Search(object sender, SearchEventArgs args)
        {
            var movies = _movieBaseService.SearchMovies(args.SearchText);
            _view.Show(movies);
        }
    }
}