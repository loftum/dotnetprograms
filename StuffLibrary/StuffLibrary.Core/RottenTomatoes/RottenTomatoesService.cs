using System.Collections.Generic;
using DotNetPrograms.Common.Web.Reading;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Common.ExtensionMethods;
using StuffLibrary.Core.RottenTomatoes.Model;

namespace StuffLibrary.Core.RottenTomatoes
{
    public class RottenTomatoesService : IRottenTomatoesService
    {
        private readonly IStuffLibrarySettings _settings;
        private readonly IHttpReader _reader;
        private readonly ApiUrls _urls;

        public RottenTomatoesService(IStuffLibrarySettings settings, IHttpReader reader)
        {
            _settings = settings;
            _reader = reader;
            _reader.Accept = "application/json";
            _urls = new ApiUrls(_settings);
        }

        public IEnumerable<RTMovie> SearchMovies(SearchParamters parameters)
        {
            var result = _reader.Get(_urls.Movies(parameters).ToString());
            var movieList = result.FromJsonTo<RTMovieList>();
            return movieList.movies;
        }
    }
}