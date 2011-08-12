using System;
using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Common.Exceptions;
using StuffLibrary.Domain;
using StuffLibrary.HtmlTools.Dropdowns;
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
            var categories = _categoryLogic.GetCategories();

            var model = new MovieViewModel(movie)
                            {
                                AvailableCategories = SelectableList.Of(categories, c => new SelectListItem{Text = c.Name, Value = c.Id.ToString()}).Items
                            };
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