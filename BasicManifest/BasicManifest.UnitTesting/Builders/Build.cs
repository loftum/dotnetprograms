using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public class Build
    {
        public static CampBuilder Camp()
        {
            return new CampBuilder();
        }

        public static PersonBuilder Person()
        {
            return new PersonBuilder();
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

        public static PersonBuilder Instructor()
        {
            return Person().WithRole(PersonRole.Instructor);
        }

        public static PersonBuilder Student()
        {
            return Person().WithRole(PersonRole.Participant);
        }
    }
}