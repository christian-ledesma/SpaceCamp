using AutoMapper;
using MyProfile = SpaceCamp.Application.Features.Profiles.Profile;
using SpaceCamp.Application.Features.Activities;
using SpaceCamp.Domain.Entities;
using System.Linq;

namespace SpaceCamp.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>().ReverseMap();
            CreateMap<Activity, ActivityDto>()
                .ForMember(x => x.HostUsername, y => y.MapFrom(x => x.Attendees.FirstOrDefault(x => x.IsHost).User.UserName)).ReverseMap();
            CreateMap<ActivityAttendee, MyProfile>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(x => x.User.DisplayName))
                .ForMember(x => x.Username, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Bio, y => y.MapFrom(x => x.User.Bio)).ReverseMap();
        }
    }
}
