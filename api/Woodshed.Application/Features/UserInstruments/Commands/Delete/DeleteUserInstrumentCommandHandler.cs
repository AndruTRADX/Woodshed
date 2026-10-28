using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.UserInstruments.Commands.Delete;

public class DeleteUserInstrumentCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, ICacheService cache) : IRequestHandler<DeleteUserInstrumentCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteUserInstrumentCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var response = await unitOfWork.Repository<UserInstrument>().GetFirstAsync
            (x => x.UserId.Equals(userId) && x.InstrumentId.Equals(request.InstrumentId))
            ?? throw new NotFoundException(nameof(UserInstrument), $"{request.InstrumentId}");

        unitOfWork.Repository<UserInstrument>().DeleteEntity(response);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rows <= 0)
            throw new ApplicationException("The instrument of the user could not be deleted");

        cache.Remove(CacheKeys.UserInstrument(userId));

        return new ApiResponse<Unit>();
    }
}
