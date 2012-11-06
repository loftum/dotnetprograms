namespace StuffLibrary.Core.Domain
{
    public class Movie : DomainObject
    {
        public int RtId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string MpaaRating { get; set; }

        public int Runtime { get; set; }
        public string CriticsConsensus { get; set; }
        public string Synopsis { get; set; }

        public MovieLinks Links { get; set; }

        public Movie()
        {
            Links = new MovieLinks();
        }
    }
}