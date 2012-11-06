using System.Collections.Generic;
using DotNetPrograms.Common.ExtensionMethods;
using StuffLibrary.Core.Domain;

namespace StuffLibrary.Lib.RottenTomatoes.Model
{
    public class RTMovie
    {
        public string id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string mpaa_rating { get; set; }
        public int runtime { get; set; }
        public string critics_consensus { get; set; }
        public string synopsis { get; set; }
        public RTReleaseDates release_dates { get; set; }
        public RTAlternateIds alternate_ids { get; set; }
        public List<RTCast> abridged_cast { get; set; }
        public RTPosters posters { get; set; }
        public RTRatings ratings { get; set; }
        public RTMovieLinks links { get; set; }

        public RTMovie()
        {
            release_dates = new RTReleaseDates();
            alternate_ids = new RTAlternateIds();
            abridged_cast = new List<RTCast>();
            posters = new RTPosters();
            ratings = new RTRatings();
            links = new RTMovieLinks();
        }

        public Movie ToMovie()
        {
            return new Movie
                {
                    RtId = id.ConvertTo<int>(),
                    Title = title,
                    Year = year,
                    MpaaRating = mpaa_rating,
                    Runtime = runtime,
                    CriticsConsensus = critics_consensus,
                    Synopsis = synopsis,
                    Links = links.ToLinks()
                };
        }
    }
}