using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.PostLikes.Commands.Delete;

public class DeletePostLikeCommand : IRequest<ApiResponse<Unit>>
{
    public required string PostId { get; set; }
}
