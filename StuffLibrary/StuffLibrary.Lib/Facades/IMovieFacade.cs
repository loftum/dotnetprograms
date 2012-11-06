using DotNetPrograms.Common.Paging;
using StuffLibrary.Core.Domain;
using StuffLibrary.Lib.RottenTomatoes;

namespace StuffLibrary.Lib.Facades
{
    public interface IMovieFacade
    {
        PagedListModel<Movie> Search(SearchParamters paramters);
    }
}