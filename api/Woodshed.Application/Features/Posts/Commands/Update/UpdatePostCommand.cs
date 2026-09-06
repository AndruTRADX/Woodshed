using MediatR;
using Woodshed.Application.Models.Request.Posts;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Posts.Commands.Update;

public class UpdatePostCommand : IRequest<ApiResponse<string>>
{
    public required string Id { get; set; }
    public required UpdatePostRequest Request { get; set; }
}
