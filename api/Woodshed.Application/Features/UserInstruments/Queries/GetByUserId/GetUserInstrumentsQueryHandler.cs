using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Caching;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.UserInstruments;
using Woodshed.Domain;

namespace Woodshed.Application.Features.UserInstruments.Queries.GetByUserId;

public class GetUserInstrumentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : IRequestHandler<GetUserInstrumentsQuery, ApiResponse<IReadOnlyList<UserInstrumentResponse>>>
{
    public async Task<ApiResponse<IReadOnlyList<UserInstrumentResponse>>> Handle(GetUserInstrumentsQuery request, CancellationToken cancellationToken)
    {
        var instruments = await cacheService.GetOrCreateAsync(
            CacheKeys.UserInstrument(request.UserId),
            async _ =>
            {
                var entities = await unitOfWork.Repository<UserInstrument>().GetAsync(predicate: x => x.UserId == request.UserId);

                var responses = mapper.Map<List<UserInstrumentResponse>>(entities);

                foreach (var instrument in responses)
                {
                    cacheService.Set(CacheKeys.UserInstrument(instrument.UserId, instrument.InstrumentId), instrument, group: CacheKeys.UserInstrumentGroup);
                }

                return responses;
            },
            group: CacheKeys.UserInstrumentGroup,
            cancellationToken: cancellationToken
        );

        return new ApiResponse<IReadOnlyList<UserInstrumentResponse>>(instruments!);
    }
}
