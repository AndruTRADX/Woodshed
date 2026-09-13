using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Application.Specifications.AccountFollowers;

namespace Woodshed.Application.Features.Account.Queries.GetPagedFollowers;

public class GetPagedFollowersQuery : AccountFollowerSpecificationParams, IRequest<ApiResponse<PagedResponse<UserAccountResponse>>>
{

}
