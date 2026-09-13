using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Follow;
using Woodshed.Application.Specifications.AccountFollowers;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Account.Queries.GetPagedFollowers;

public class GetPagedFollowersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<GetPagedFollowersQuery, ApiResponse<PagedResponse<FollowerResponse>>>
{
    public async Task<ApiResponse<PagedResponse<FollowerResponse>>> Handle(GetPagedFollowersQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserId();

        var spec = new FollowersSpecification(request);

        var data = await unitOfWork.Repository<UserFollower>()
            .GetAllWithSpec<FollowerResponse>(spec, mapper.ConfigurationProvider, cancellationToken);

        var specCount = new FollowersCountSpecification(request);
        var totalCount = await unitOfWork.Repository<UserFollower>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        return new ApiResponse<PagedResponse<FollowerResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
