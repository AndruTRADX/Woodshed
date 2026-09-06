using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;
using Woodshed.Application.Specifications.PostComments;

namespace Woodshed.Application.Features.PostComments.Queries.GetPaged;

public class GetPagedPostCommentQuery : PostCommentSpecificationParams, IRequest<ApiResponse<PagedResponse<PostCommentResponse>>>
{
    [FromRoute(Name = "postId")]
    public required string PostId { get; set; }
}
