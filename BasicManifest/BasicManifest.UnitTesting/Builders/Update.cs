using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public static class Update
    {
        public static CampBuilder Camp(Camp camp)
        {
            return new CampBuilder(camp);
        }
    }
}