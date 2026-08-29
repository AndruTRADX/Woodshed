using AutoMapper;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Application.Mappings;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatePostRequest, Post>();

        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<ApplicationUser, UserAccountResponse>();

        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.PostComments.Count))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.PostLikes.Count));
    }
}
