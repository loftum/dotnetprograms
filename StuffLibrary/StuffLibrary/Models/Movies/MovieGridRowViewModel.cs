using System.Linq;
using StuffLibrary.Attributes;
using StuffLibrary.Domain;
using StuffLibrary.Models.Grids;

namespace StuffLibrary.Models.Movies
{
    public class MovieGridRowViewModel : IGridRowViewModel
    {
        public string RowId
        {
            get { return Id.ToString(); }
        }

        [TransferToGrid]
        public long Id { get; set; }
        [TransferToGrid]
        public string Title { get; set; }
        [TransferToGrid]
        public string Category { get; set; }

        public MovieGridRowViewModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Category = string.Join(", ", movie.Categories.Select(c => c.Name));
        }
    }
}