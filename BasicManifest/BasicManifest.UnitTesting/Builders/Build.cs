using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class Build
    {
        public static CampBuilder Camp()
        {
            return new CampBuilder();
        }

        public static SkydiverBuilder Person()
        {
            return new SkydiverBuilder();
        }

        public static DayBuilder Day()
        {
            return new DayBuilder();
        }

        public static LoadBuilder Load()
        {
            return new LoadBuilder();
        }

        public static GroupBuilder Group()
        {
            return new GroupBuilder();
        }

        public static SkydiverBuilder Instructor()
        {
            return Person().WithRole(PersonRole.Instructor);
        }

        public static SkydiverBuilder Student()
        {
            return Person().WithRole(PersonRole.Participant);
        }
    }
}