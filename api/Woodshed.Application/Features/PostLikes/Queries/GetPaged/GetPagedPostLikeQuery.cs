using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostLikes;
using Woodshed.Application.Specifications.PostLikes;

namespace Woodshed.Application.Features.PostLikes.Queries.GetPaged;

public class GetPagedPostLikeQuery : PostLikeSpecificationParams, IRequest<ApiResponse<PagedResponse<PostLikeResponse>>>
{
    [FromRoute(Name = "postId")]
    public required string PostId { get; set; }
}
