using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Instruments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Instruments.Queries.GetById;

public class GetInstrumentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) : IRequestHandler<GetInstrumentByIdQuery, ApiResponse<InstrumentResponse>>
{
    public async Task<ApiResponse<InstrumentResponse>> Handle(GetInstrumentByIdQuery request, CancellationToken cancellationToken)
    {
        var instrument = await cache.GetOrCreateAsync(
            CacheKeys.Instrument(request.Id),
            async _ =>
            {
                var entity = await unitOfWork.Repository<Instrument>()
                    .GetFirstAsync(x => x.Id == request.Id);

                return entity is null ? null : mapper.Map<InstrumentResponse>(entity);
            },
            group: CacheKeys.InstrumentsGroup,
            cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(Instrument), request.Id);

        return new ApiResponse<InstrumentResponse>(instrument);
    }
}
