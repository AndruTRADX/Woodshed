using MediatR;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Posts.Commands.Create;

public class CreatePostCommand : IRequest<ApiResponse<string>>
{
    public required CreatePostRequest Request { get; set; }
}
