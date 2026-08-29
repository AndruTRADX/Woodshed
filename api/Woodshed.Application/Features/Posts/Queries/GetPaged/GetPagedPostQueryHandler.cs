using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Posts;
using Woodshed.Application.Specifications.Posts;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Queries.GetPaged;

public class GetPagedPostQueryHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<GetPagedPostQuery, ApiResponse<PagedResponse<PostResponse>>>
{
    public async Task<ApiResponse<PagedResponse<PostResponse>>> Handle(GetPagedPostQuery request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault();
        var spec = new PostSpecification(request, userId);
        var response = await unitOfWork.Repository<Post>().GetAllWithSpec(spec);

        var specCount = new PostCountSpecification(request, userId);
        var totalCount = await unitOfWork.Repository<Post>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<Post>, IReadOnlyList<PostResponse>>(response);

        return new ApiResponse<PagedResponse<PostResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
