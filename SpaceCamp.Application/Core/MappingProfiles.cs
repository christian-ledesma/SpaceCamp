using AutoMapper;
using SpaceCamp.Application.Features.Activities;
using SpaceCamp.Domain.Entities;
using System.Linq;
using MyProfile = SpaceCamp.Application.Features.Profiles.Profile;

namespace SpaceCamp.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>().ReverseMap();
            CreateMap<Activity, ActivityDto>()
                .ForMember(x => x.HostUsername, y => y.MapFrom(x => x.Attendees.FirstOrDefault(x => x.IsHost).User.UserName)).ReverseMap();
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(x => x.User.DisplayName))
                .ForMember(x => x.Username, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Bio, y => y.MapFrom(x => x.User.Bio))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.User.Photos.FirstOrDefault(a => a.IsMain).Url))
                .ReverseMap();
            CreateMap<User, MyProfile>()
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Photos.FirstOrDefault(a => a.IsMain).Url))
                .ReverseMap();
        }
    }
}
