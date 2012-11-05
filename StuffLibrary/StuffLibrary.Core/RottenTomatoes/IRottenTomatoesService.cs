using System.Collections.Generic;
using StuffLibrary.Core.RottenTomatoes.Model;

namespace StuffLibrary.Core.RottenTomatoes
{
    public interface IRottenTomatoesService
    {
        IEnumerable<RTMovie> SearchMovies(SearchParamters parameters);
    }
}