using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.PostLikes;
using Woodshed.Application.Specifications.PostLikes;
using Woodshed.Domain;

namespace Woodshed.Application.Features.PostLikes.Queries.GetPaged;

public class GetPagedPostLikeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetPagedPostLikeQuery, ApiResponse<PagedResponse<PostLikeResponse>>>
{
    public async Task<ApiResponse<PagedResponse<PostLikeResponse>>> Handle(GetPagedPostLikeQuery request, CancellationToken cancellationToken)
    {
        var spec = new PostLikeSpecification(request, request.PostId);

        var data = await unitOfWork.Repository<PostLike>()
            .GetAllWithSpec<PostLikeResponse>(spec, mapper.ConfigurationProvider, cancellationToken);

        var specCount = new PostLikeCountSpecification(request, request.PostId);
        var totalCount = await unitOfWork.Repository<PostLike>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        return new ApiResponse<PagedResponse<PostLikeResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
