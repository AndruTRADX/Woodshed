using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQueryHandler(IUserAccountService accountService) : IRequestHandler<GetAccountByIdQuery, ApiResponse<UserAccountResponse>>
{
    public async Task<ApiResponse<UserAccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await accountService.GetUserAccount(request.UserId, cancellationToken);

        return new ApiResponse<UserAccountResponse>(account);
    }
}
