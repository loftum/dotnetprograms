using DotNetPrograms.Common.Web.Paths;
using StuffLibrary.Common.Configuration;

namespace StuffLibrary.Lib.RottenTomatoes
{
    public class ApiUrls
    {
        private readonly IStuffLibrarySettings _settings;

        public ApiUrls(IStuffLibrarySettings settings)
        {
            _settings = settings;
        }

        public WebUrl BaseUrl
        {
            get
            {
                return new WebUrl("http://api.rottentomatoes.com/api/public/v1.0")
                    .WithParameter("apikey", _settings.RottenTomatoesApiKey)
                    .WithExtension("json");
            }
        }

        public WebUrl Movies(SearchParamters paramters)
        {
            return BaseUrl
                .WithParameter("q", paramters.Query)
                .AppendToPath("movies")
                .WithParameter("page", paramters.PageNumber)
                .WithParameter("page_limit", paramters.PageSize);
        }
    }
}