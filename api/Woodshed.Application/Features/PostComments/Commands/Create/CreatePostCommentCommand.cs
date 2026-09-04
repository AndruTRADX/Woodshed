using MediatR;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.PostComments.Commands.Create;

public class CreatePostCommentCommand : IRequest<ApiResponse<string>>
{
    public required string PostId { get; set; }
    public required CreatePostCommentRequest Request { get; set; }
}
