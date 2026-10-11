using MediatR;
using Woodshed.Application.Models.Request.PostComments;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;

namespace Woodshed.Application.Features.PostComments.Commands.Create;

public class CreatePostCommentCommand : IRequest<ApiResponse<PostCommentResponse>>
{
    public required string PostId { get; set; }
    public required CreatePostCommentRequest Request { get; set; }
}
