using System.Collections.Generic;

namespace StuffLibrary.Lib.RottenTomatoes.Model
{
    public class RTMovieList
    {
        public List<RTMovie> movies { get; set; }

        public RTMovieList()
        {
            movies = new List<RTMovie>();
        }
    }
}