using System.Collections.Generic;

namespace BasicManifest.Lib.Models
{
    public class CampIndexModel
    {
        public IEnumerable<CampModel> Camps { get; private set; }

        public CampIndexModel(IEnumerable<CampModel> camps)
        {
            Camps = camps;
        }
    }
}