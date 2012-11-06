using System.Collections.Generic;
using StuffLibrary.Lib.RottenTomatoes.Model;

namespace StuffLibrary.Lib.RottenTomatoes
{
    public interface IRottenTomatoesService
    {
        IEnumerable<RTMovie> SearchMovies(SearchParamters parameters);
    }
}