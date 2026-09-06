using AutoMapper;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Identity;
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
        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<ApplicationUser, UserAccountResponse>();

        CreateMap<CreatePostRequest, Post>();
        CreateMap<Post, PostResponse>()
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count));

        CreateMap<CreatePostCommentRequest, PostComment>();
        CreateMap<UpdatePostCommentRequest, PostComment>();
        CreateMap<PostComment, PostCommentResponse>();

        CreateMap<PostLike, PostLikeResponse>();
    }
}
