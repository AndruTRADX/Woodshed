using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQueryHandler(IUserAccountService accountService, IUserAccessor userAccessor) : IRequestHandler<GetAccountByIdQuery, ApiResponse<UserAccountResponse>>
{
    public async Task<ApiResponse<UserAccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var account = await accountService.GetUserAccount(request.UserId, cancellationToken, new { currentUserId });

        return new ApiResponse<UserAccountResponse>(account);
    }
}
