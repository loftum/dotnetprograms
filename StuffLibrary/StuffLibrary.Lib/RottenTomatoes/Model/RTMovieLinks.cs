using StuffLibrary.Core.Domain;

namespace StuffLibrary.Lib.RottenTomatoes.Model
{
    public class RTMovieLinks
    {
        public string self { get; set; }
        public string alternate { get; set; }
        public string cast { get; set; }
        public string clips { get; set; }
        public string reviews { get; set; }
        public string similar { get; set; }

        public MovieLinks ToLinks()
        {
            return new MovieLinks
                {
                    Self = self,
                    Alternate = alternate,
                    Cast = cast,
                    Clips = clips,
                    Reviews = reviews,
                    Similar = similar
                };
        }
    }
}