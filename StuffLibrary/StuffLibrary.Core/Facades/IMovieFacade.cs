using DotNetPrograms.Common.Paging;
using StuffLibrary.Core.Domain;
using StuffLibrary.Core.RottenTomatoes;

namespace StuffLibrary.Core.Facades
{
    public interface IMovieFacade
    {
        PagedListModel<Movie> Search(SearchParamters paramters);
    }
}