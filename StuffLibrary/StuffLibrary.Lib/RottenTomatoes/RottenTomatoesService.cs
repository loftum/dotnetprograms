using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Web.Reading;
using Newtonsoft.Json;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Common.ExtensionMethods;
using StuffLibrary.Lib.RottenTomatoes.Model;

namespace StuffLibrary.Lib.RottenTomatoes
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
            if (!parameters.HasQuery)
            {
                return Enumerable.Empty<RTMovie>();
            }
            var url = _urls.Movies(parameters);
            var result = _reader.Get(url.ToString());
            var movieList = result.FromJsonTo<RTMovieList>();
            return movieList.movies;
        }
    }
}