using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Common.Exceptions;
using StuffLibrary.Lib.BusinessLogic;
using StuffLibrary.Models.Grids;
using StuffLibrary.Models.Movies;

namespace StuffLibrary.Controllers
{
    public class MovieController : StuffLibraryControllerBase
    {
        private readonly IMovieLogic _movieLogic;

        public MovieController(IMovieLogic movieLogic)
        {
            _movieLogic = movieLogic;
        }

        public ActionResult Index()
        {
            return View(new MovieIndexViewModel());
        }

        public ActionResult JsonMovies()
        {
            var movies = from movie 
                    in _movieLogic.GetAllMovies()
                    select new MovieGridRowViewModel(movie);
            return Json(new GridViewModel(movies), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterNew()
        {
            var model = new MovieViewModel();
            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var movie = _movieLogic.GetMovie(id);
            var model = new MovieViewModel(movie);
            return View(model);
        }

        public ActionResult Save(MovieViewModel model)
        {
            try
            {
                _movieLogic.Save(model.Movie);
                return RedirectToAction("Index");
            }
            catch (UserException ex)
            {
                AddFlashMessageFor(ex);
                return View("Edit", model);
            }
        }
    }
}