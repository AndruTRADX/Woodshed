using AutoMapper;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Identity;
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
    }
}
