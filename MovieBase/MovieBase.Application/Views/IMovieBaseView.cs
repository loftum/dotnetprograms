using System.Collections.Generic;
using MovieBase.AppLib.Events;
using MovieBase.Domain;

namespace MovieBase.AppLib.Views
{
    public interface IMovieBaseView
    {
        event SearchEventHandler Search;
        void Show(IEnumerable<Movie> movies);
    }
    public delegate void SearchEventHandler(object sender, SearchEventArgs args);
}