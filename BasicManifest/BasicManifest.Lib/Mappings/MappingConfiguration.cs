using AutoMapper;
using DotNetPrograms.Common.ExtensionMethods;

namespace BasicManifest.Lib.Mappings
{
    public static class MappingConfiguration
    {
        public static void Init(Profile profile, params Profile[] additionalProfiles)
        {
            var profiles = profile.ToListWith(additionalProfiles);
            foreach (var p in profiles)
            {
                Mapper.AddProfile(p);
            }
        }

        public static void Reset()
        {
            Mapper.Reset();
        }
    }
}