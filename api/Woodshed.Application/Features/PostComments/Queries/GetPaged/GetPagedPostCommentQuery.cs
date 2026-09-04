using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostComments;
using Woodshed.Application.Specifications.PostComments;

namespace Woodshed.Application.Features.PostComments.Queries.GetPaged;

public class GetPagedPostCommentQuery : PostCommentSpecificationParams, IRequest<ApiResponse<PagedResponse<PostCommentResponse>>>
{
    public required string PostId { get; set; }
}
