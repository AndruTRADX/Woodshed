using AutoMapper;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Follow;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Application.Models.Response.Photos;
using Woodshed.Application.Models.Response.PostComments;
using Woodshed.Application.Models.Response.PostLikes;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string? currentUserId = null;

        CreateMap<ApplicationUser, UserResponse>();

        CreateMap<UserFollower, FolloweeResponse>();
        CreateMap<UserFollower, FollowerResponse>();
        CreateMap<ApplicationUser, UserAccountResponse>()
            .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest => dest.FollowingsCount, opt => opt.MapFrom(src => src.Following.Count))
            .ForMember(dest => dest.IsFollower, opt => opt.MapFrom(src => src.Following.Any(f => f.FolloweeId == currentUserId)))
            .ForMember(dest => dest.IsFollowee, opt => opt.MapFrom(src => src.Followers.Any(f => f.FollowerId == currentUserId)));

        CreateMap<CreatePostRequest, Post>();
        CreateMap<UpdatePostRequest, Post>();
        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.IsLiked, opt => opt.MapFrom(src => src.Likes.Any(f => f.UserId == currentUserId)));

        CreateMap<CreatePostCommentRequest, PostComment>();
        CreateMap<UpdatePostCommentRequest, PostComment>();
        CreateMap<PostComment, PostCommentResponse>();

        CreateMap<PostLike, PostLikeResponse>();

        CreateMap<Photo, PhotoResponse>();
    }
}
