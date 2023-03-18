using AutoMapper;
using SpaceCamp.Application.Features.Activities;
using SpaceCamp.Application.Features.Comments;
using SpaceCamp.Domain.Entities;
using System.Linq;
using MyProfile = SpaceCamp.Application.Features.Profiles.Profile;

namespace SpaceCamp.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Activity, Activity>().ReverseMap();
            CreateMap<Activity, ActivityDto>()
                .ForMember(x => x.HostUsername, y => y.MapFrom(x => x.Attendees.FirstOrDefault(x => x.IsHost).User.UserName)).ReverseMap();
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(x => x.User.DisplayName))
                .ForMember(x => x.Username, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Bio, y => y.MapFrom(x => x.User.Bio))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.User.Photos.FirstOrDefault(a => a.IsMain).Url))
                .ForMember(x => x.FollowersCount, y => y.MapFrom(z => z.User.Followers.Count))
                .ForMember(x => x.FollowingCount, y => y.MapFrom(z => z.User.Followings.Count))
                .ForMember(x => x.Following, y => y.MapFrom(z => z.User.Followers.Any(z => z.Observer.UserName == currentUsername)))
                .ReverseMap();
            CreateMap<User, MyProfile>()
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Photos.FirstOrDefault(a => a.IsMain).Url))
                .ForMember(x => x.FollowersCount, y => y.MapFrom(z => z.Followers.Count))
                .ForMember(x => x.FollowingCount, y => y.MapFrom(z => z.Followings.Count))
                .ForMember(x => x.Following, y => y.MapFrom(z => z.Followers.Any(z => z.Observer.UserName == currentUsername)))
                .ReverseMap();
            CreateMap<Comment, CommentDto>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(x => x.Author.DisplayName))
                .ForMember(x => x.Username, y => y.MapFrom(x => x.Author.UserName))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Author.Photos.FirstOrDefault(a => a.IsMain).Url))
                .ReverseMap();
        }
    }
}
