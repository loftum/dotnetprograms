using System.Collections.Generic;
using System.Windows;
using MovieBase.Domain;
using MovieBase.Events;

namespace MovieBase.Views
{
    public partial class MovieBaseWindow : IMovieBaseView
    {
        public event SearchEventHandler Search;

        public MovieBaseWindow()
        {
            InitializeComponent();
        }

        public void Show(IEnumerable<Movie> movies)
        {
            MovieGrid.Items.Clear();
            foreach (var movie in movies)
            {
                MovieGrid.Items.Add(movie);
            }
        }

// ReSharper disable InconsistentNaming
        private void SearchButton_Click(object sender, RoutedEventArgs e)
// ReSharper restore InconsistentNaming
        {
            if (Search != null)
            {
                Search.Invoke(sender, new SearchEventArgs(SearchBox.Text));
            }
        }
    }
}
