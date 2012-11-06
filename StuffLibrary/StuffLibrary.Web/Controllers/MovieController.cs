using System.Web.Mvc;
using StuffLibrary.Lib.Facades;
using StuffLibrary.Lib.RottenTomatoes;
using StuffLibrary.Web.Models;

namespace StuffLibrary.Web.Controllers
{
    public class MovieController : StuffLibraryControllerBase
    {
        private readonly IMovieFacade _movieFacade;

        public MovieController(IMovieFacade movieFacade)
        {
            _movieFacade = movieFacade;
        }

        public ActionResult Index()
        {
            return View(new MovieSearchViewModel());
        }

        public ActionResult Search(MovieSearchViewModel model)
        {
            var movies = _movieFacade.Search(new SearchParamters(model.Query));
            return PartialView("MovieList", movies);
        }
    }
}