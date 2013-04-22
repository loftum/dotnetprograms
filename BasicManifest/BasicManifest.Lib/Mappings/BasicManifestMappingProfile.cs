using AutoMapper;
using BasicManifest.Core.Domain;
using BasicManifest.Lib.Models;

namespace BasicManifest.Lib.Mappings
{
    public class BasicManifestMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Camp, CampModel>();
        }
    }
}