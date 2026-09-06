using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.PostLikes.Commands.Create;

public class CreatePostLikeCommand : IRequest<ApiResponse<string>>
{
    public required string PostId { get; set; }
}
