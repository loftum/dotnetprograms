using System.Collections.Generic;

namespace StuffLibrary.Lib.RottenTomatoes.Model
{
    public class RTCast
    {
        public string name { get; set; }
        public string id { get; set; }
        public List<string> characters { get; set; }

        public RTCast()
        {
            characters = new List<string>();
        }
    }
}