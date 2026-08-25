using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Features.Identities.Queries.GetAccount;

public class GetAccountQuery : IRequest<ApiResponse<UserResponse?>>
{

}
