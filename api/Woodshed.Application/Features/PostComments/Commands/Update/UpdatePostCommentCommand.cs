using MediatR;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.PostComments.Commands.Update;

public class UpdatePostCommentCommand : IRequest<ApiResponse<string>>
{
    public required string PostId { get; set; }
    public required string CommentId { get; set; }
    public required UpdatePostCommentRequest Request { get; set; }
}
