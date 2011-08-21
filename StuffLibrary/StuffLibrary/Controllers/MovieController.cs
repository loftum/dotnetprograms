using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Common.Exceptions;
using StuffLibrary.Domain;
using StuffLibrary.Lib.BusinessLogic;
using StuffLibrary.Models.Grids;
using StuffLibrary.Models.Movies;

namespace StuffLibrary.Controllers
{
    public class MovieController : StuffLibraryControllerBase
    {
        private readonly IMovieLogic _movieLogic;
        private readonly ICategoryLogic _categoryLogic;

        public MovieController(IMovieLogic movieLogic, ICategoryLogic categoryLogic)
        {
            _movieLogic = movieLogic;
            _categoryLogic = categoryLogic;
        }

        public ActionResult Index()
        {
            return View(new MovieIndexViewModel());
        }

        public ActionResult JsonMovies(JqGridParameters parameters)
        {
            var movies = from movie 
                    in _movieLogic.GetAllMovies(parameters.Query)
                    select new MovieGridRowViewModel(movie);
            return Json(new JqGridViewModel(parameters, movies), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterNew()
        {
            var model = ModelFor(new Movie());
            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var movie = _movieLogic.GetMovie(id);
            var model = ModelFor(movie);
            return View(model);
        }

        private MovieViewModel ModelFor(Movie movie)
        {
            var categories = _categoryLogic.GetCategories();
            return new MovieViewModel(movie)
            {
                AvailableCategories = from c in categories select c.Name
            };
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