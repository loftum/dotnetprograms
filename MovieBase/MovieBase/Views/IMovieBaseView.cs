using System.Collections.Generic;
using MovieBase.Domain;
using MovieBase.Events;

namespace MovieBase.Views
{
    public interface IMovieBaseView
    {
        event SearchEventHandler Search;
        void Show(IEnumerable<Movie> movies);
    }
    public delegate void SearchEventHandler(object sender, SearchEventArgs args);
}