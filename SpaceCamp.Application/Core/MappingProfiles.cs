using AutoMapper;
using SpaceCamp.Domain.Entities;

namespace SpaceCamp.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>().ReverseMap();
        }
    }
}
