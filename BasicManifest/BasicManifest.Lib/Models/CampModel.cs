using System.Collections.Generic;

namespace BasicManifest.Lib.Models
{
    public class CampModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<SkydiverModel> Skydivers { get; set; }

        public CampModel()
        {
            Skydivers = new List<SkydiverModel>();
        }
    }
}