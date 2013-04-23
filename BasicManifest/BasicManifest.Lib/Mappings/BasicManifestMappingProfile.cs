using AutoMapper;
using BasicManifest.Core.Domain;
using BasicManifest.Lib.ExtensionMethods;
using BasicManifest.Lib.Models;

namespace BasicManifest.Lib.Mappings
{
    public class BasicManifestMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Camp, CampModel>();
            CreateMap<Skydiver, SkydiverModel>()
                .Ignore(m => m.FullName);
        }
    }
}