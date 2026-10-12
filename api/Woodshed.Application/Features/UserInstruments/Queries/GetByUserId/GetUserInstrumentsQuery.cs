using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.UserInstruments;

namespace Woodshed.Application.Features.UserInstruments.Queries.GetByUserId;

public class GetUserInstrumentsQuery : IRequest<ApiResponse<IReadOnlyList<UserInstrumentResponse>>>
{
    public required string UserId { get; set; }
}
