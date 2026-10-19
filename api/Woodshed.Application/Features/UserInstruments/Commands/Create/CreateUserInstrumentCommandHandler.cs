using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.UserInstruments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.UserInstruments.Commands.Create;

public class CreateUserInstrumentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, ICacheService cache) : IRequestHandler<CreateUserInstrumentCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CreateUserInstrumentCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        _ = await unitOfWork.Repository<Instrument>().GetFirstAsync(x => x.Id == request.Request.InstrumentId)
            ?? throw new NotFoundException(nameof(Instrument), request.Request.InstrumentId);
        
        _ = await unitOfWork.Repository<InstrumentProficiency>().GetFirstAsync(x => x.Code == request.Request.ProficiencyId)
            ?? throw new NotFoundException(nameof(InstrumentProficiency), request.Request.InstrumentId);

        var data = mapper.Map<UserInstrument>(request.Request);
        data.UserId = userId;

        unitOfWork.Repository<UserInstrument>().AddEntity(data);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rows <= 0)
            throw new ApplicationException("The instrument could not be added to the user");

        cache.Remove(CacheKeys.UserInstrument(userId));

        return new ApiResponse<Unit>();
    }
}