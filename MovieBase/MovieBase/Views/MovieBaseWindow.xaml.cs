using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using MovieBase.AppLib.Events;
using MovieBase.AppLib.Views;
using MovieBase.Domain;

namespace MovieBase.Views
{
    public partial class MovieBaseWindow : IMovieBaseView
    {
        public ObservableCollection<Movie> Movies { get; private set; }

        public event SearchEventHandler Search;

        public MovieBaseWindow()
        {
            InitializeComponent();
            Movies = new ObservableCollection<Movie>();
            MovieGrid.ItemsSource = Movies;
        }

        public void Show(IEnumerable<Movie> movies)
        {
            Movies.Clear();
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }
        }

        private void InvokeSearch(object sender)
        {
            if (Search != null)
            {
                Search.Invoke(sender, new SearchEventArgs(SearchBox.Text));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            InvokeSearch(sender);
        }

        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            InvokeSearch(sender);
        }
    }
}
