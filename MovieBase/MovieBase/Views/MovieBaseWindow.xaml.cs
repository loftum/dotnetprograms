using System.Collections.Generic;
using System.Windows;
using MovieBase.AppLib.Events;
using MovieBase.AppLib.Views;
using MovieBase.Domain;

namespace MovieBase.Views
{
    public partial class MovieBaseWindow : IMovieBaseView
    {
        public IEnumerable<Movie> Movies { get; private set; }

        public event SearchEventHandler Search;

        public MovieBaseWindow()
        {
            InitializeComponent();
            Movies = new Movie[0];
            MovieGrid.ItemsSource = Movies;
        }

        public void Show(IEnumerable<Movie> movies)
        {
            Movies = movies;
            MovieGrid.ItemsSource = Movies;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (Search != null)
            {
                Search.Invoke(sender, new SearchEventArgs(SearchBox.Text));
            }
        }
    }
}
