using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Identities.Queries.GetAccount;

public class GetAccountQueryHandler(IUserAccessor userAccessor) : IRequestHandler<GetAccountQuery, ApiResponse<UserResponse?>>
{
    public async Task<ApiResponse<UserResponse?>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetCurrentUserAsync();

        return new ApiResponse<UserResponse?>(user);
    }
}
