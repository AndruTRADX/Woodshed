using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.PostComments.Commands.Delete;

public class DeletePostCommentCommand : IRequest<ApiResponse<Unit>>
{
    public required string PostId { get; set; }
    public required string CommentId { get; set; }
}
