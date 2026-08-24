using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.Application.Features.Identities.Commands.SignOut;

public class SignOutUserCommand: IRequest<ApiResponse<Unit>>
{

}