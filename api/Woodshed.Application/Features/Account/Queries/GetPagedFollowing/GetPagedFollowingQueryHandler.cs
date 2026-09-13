using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Application.Specifications.AccountFollowers;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Account.Queries.GetPagedFollowing;

public class GetPagedFollowingQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetPagedFollowingQuery, ApiResponse<PagedResponse<UserAccountResponse>>>
{
    public async Task<ApiResponse<PagedResponse<UserAccountResponse>>> Handle(GetPagedFollowingQuery request, CancellationToken cancellationToken)
    {
        var spec = new FollowingSpecification(request);

        var data = await unitOfWork.Repository<UserFollower>()
            .GetAllWithSpec<UserAccountResponse>(spec, mapper.ConfigurationProvider, cancellationToken);

        var specCount = new FollowingCountSpecification(request);
        var totalCount = await unitOfWork.Repository<UserFollower>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        return new ApiResponse<PagedResponse<UserAccountResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
