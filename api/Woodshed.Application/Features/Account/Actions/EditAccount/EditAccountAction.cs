using MediatR;
using Woodshed.Application.Models.Request.Identity;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Account.Actions.EditAccount;

public class EditAccountAction : IRequest<ApiResponse<UserAccountResponse>>
{
    public required EditAccountRequest Request { get; set; }
}
