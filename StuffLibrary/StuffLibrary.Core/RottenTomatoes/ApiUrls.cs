using DotNetPrograms.Common.Web;
using StuffLibrary.Common.Configuration;

namespace StuffLibrary.Core.RottenTomatoes
{
    public class ApiUrls : IApiUrls
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
                    .WithParameter("apikey", _settings.RottenTomatoesApiKey);
            }
        }


        public WebUrl Movies(SearchParamters paramters)
        {
            return BaseUrl.WithParameter("q", paramters.Query)
                .WithParameter("page", paramters.PageNumber)
                .WithParameter("page_limit", paramters.PageSize);
        }
    }
}