using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Follow;
using Woodshed.Application.Specifications.AccountFollowers;

namespace Woodshed.Application.Features.Account.Queries.GetPagedFollowing;

public class GetPagedFollowingQuery : AccountFollowerSpecificationParams, IRequest<ApiResponse<PagedResponse<FolloweeResponse>>>
{

}
