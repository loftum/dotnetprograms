using System.Linq;
using DotNetPrograms.Common.Paging;
using StuffLibrary.Core.Domain;
using StuffLibrary.Lib.Caching;
using StuffLibrary.Lib.RottenTomatoes;

namespace StuffLibrary.Lib.Facades
{
    public class MovieFacade : IMovieFacade
    {
        private readonly IRottenTomatoesService _rottenTomatoes;
        private readonly IStuffLibraryCache _cache;

        public MovieFacade(IRottenTomatoesService rottenTomatoes,
            IStuffLibraryCache cache)
        {
            _rottenTomatoes = rottenTomatoes;
            _cache = cache;
        }

        public PagedListModel<Movie> Search(SearchParamters paramters)
        {
            if (!paramters.HasQuery)
            {
                return PagedListModel<Movie>.Empty<Movie>();
            }

            var movies = _cache.Get(() => _rottenTomatoes.SearchMovies(paramters), paramters.GetCacheKey());
            return new PagedListModel<Movie>(movies.Select(m => m.ToMovie()), paramters.PageNumber, paramters.PageSize);
        }
    }
}