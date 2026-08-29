using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Posts;

namespace Woodshed.Application.Features.Posts.Queries.GetById;

public class GetPostByIdQuery : IRequest<ApiResponse<PostResponse>>
{
    public required string Id { get; set; }
}
