using System.Linq;
using DotNetPrograms.Common.Paging;
using StuffLibrary.Core.Domain;
using StuffLibrary.Core.RottenTomatoes;

namespace StuffLibrary.Core.Facades
{
    public class MovieFacade : IMovieFacade
    {
        private readonly IRottenTomatoesService _rottenTomatoes;

        public MovieFacade(IRottenTomatoesService rottenTomatoes)
        {
            _rottenTomatoes = rottenTomatoes;
        }

        public PagedListModel<Movie> Search(SearchParamters paramters)
        {
            var movies = _rottenTomatoes.SearchMovies(paramters);
            return new PagedListModel<Movie>(movies.Select(m => m.ToMovie()), paramters.PageNumber, paramters.PageSize);
        }
    }
}