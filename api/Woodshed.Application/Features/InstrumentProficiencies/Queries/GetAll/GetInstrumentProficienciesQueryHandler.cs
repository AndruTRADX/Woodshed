using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.InstrumentProficiencies;
using Woodshed.Domain;

namespace Woodshed.Application.Features.InstrumentProficiencies.Queries.GetAll;

public class GetInstrumentProficienciesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : IRequestHandler<GetInstrumentProficienciesQuery, ApiResponse<IReadOnlyList<InstrumentProficiencyResponse>>>
{
    public async Task<ApiResponse<IReadOnlyList<InstrumentProficiencyResponse>>> Handle(GetInstrumentProficienciesQuery request, CancellationToken cancellationToken)
    {
        var proficiencies = await cacheService.GetOrCreateAsync<IReadOnlyList<InstrumentProficiencyResponse>>(
            CacheKeys.InstrumentProficiency,
            async _ =>
            {
                var entities = await unitOfWork.Repository<InstrumentProficiency>().GetAsync(orderBy: x => x.OrderBy(x => x.Order));

                var responses = mapper.Map<List<InstrumentProficiencyResponse>>(entities);

                foreach (var proficiency in responses)
                {
                    cacheService.Set(CacheKeys.InstrumentProficiency, proficiency, group: CacheKeys.InstrumentProficiencyGroup);
                }

                return responses;
            },
            group: CacheKeys.InstrumentProficiencyGroup,
            cancellationToken: cancellationToken
        );

        return new ApiResponse<IReadOnlyList<InstrumentProficiencyResponse>>(proficiencies!);
    }
}
