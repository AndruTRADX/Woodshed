using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQuery : IRequest<ApiResponse<UserAccountResponse>>
{
    public string UserId { get; set; } = string.Empty;
}

