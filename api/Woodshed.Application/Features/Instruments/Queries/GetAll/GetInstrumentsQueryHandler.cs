using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Instruments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Instruments.Queries.GetAll;

public class GetInstrumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache) : IRequestHandler<GetInstrumentsQuery, ApiResponse<IReadOnlyList<InstrumentResponse>>>
{
    public async Task<ApiResponse<IReadOnlyList<InstrumentResponse>>> Handle(GetInstrumentsQuery request, CancellationToken cancellationToken)
    {
        var instruments = await cache.GetOrCreateAsync<IReadOnlyList<InstrumentResponse>>(
            CacheKeys.Instruments,
            async _ =>
            {
                var entities = await unitOfWork.Repository<Instrument>()
                    .GetAsync(orderBy: x => x.OrderBy(i => i.Name));

                var responses = mapper.Map<List<InstrumentResponse>>(entities);

                foreach (var instrument in responses)
                {
                    cache.Set(CacheKeys.Instrument(instrument.Id), instrument, group: CacheKeys.InstrumentsGroup);
                }

                return responses;
            },
            group: CacheKeys.InstrumentsGroup,
            cancellationToken: cancellationToken);

        return new ApiResponse<IReadOnlyList<InstrumentResponse>>(instruments!);
    }
}
