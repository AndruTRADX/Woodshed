using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Application.Specifications.Posts;

namespace Woodshed.Application.Features.Posts.Queries.GetPaged;

public class GetPagedPostQuery : PostSpecificationParams, IRequest<ApiResponse<PagedResponse<PostResponse>>>
{

}
