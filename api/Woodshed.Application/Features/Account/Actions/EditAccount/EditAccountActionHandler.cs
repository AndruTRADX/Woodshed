using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Account.Actions.EditAccount;

public class EditAccountActionHandler(IUserAccessor userAccessor, IUserAccountService userAccountService) : IRequestHandler<EditAccountAction, ApiResponse<UserAccountResponse>>
{
    public async Task<ApiResponse<UserAccountResponse>> Handle(EditAccountAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var account = await userAccountService.EditAccountAsync(
            userId,
            request.Request.NickName,
            request.Request.Name,
            request.Request.LastName,
            request.Request.Biography,
            cancellationToken);

        return new ApiResponse<UserAccountResponse>(account);
    }
}
