using DotNetPrograms.Common.ExtensionMethods;
using StuffLibrary.Core.Domain;

namespace StuffLibrary.Core.RottenTomatoes.Model
{
    public class RTMovie
    {
        public string id { get; set; }
        public string title { get; set; }
        public int year { get; set; }

        public Movie ToMovie()
        {
            return new Movie
                {
                    Id = id.ConvertTo<int>(),
                    Title = title,
                    Year = year
                };
        }
    }
}