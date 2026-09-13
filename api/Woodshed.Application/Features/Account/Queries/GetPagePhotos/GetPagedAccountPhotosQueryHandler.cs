using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Photos;
using Woodshed.Application.Specifications.Photos;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Account.Queries.GetPagePhotos;

public class GetPagedAccountPhotosQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetPagedAccountPhotosQuery, ApiResponse<PagedResponse<PhotoResponse>>>
{
    public async Task<ApiResponse<PagedResponse<PhotoResponse>>> Handle(GetPagedAccountPhotosQuery request, CancellationToken cancellationToken)
    {
        var spec = new PhotoSpecification(request);
        var response = await unitOfWork.Repository<Photo>().GetAllWithSpec(spec);

        var specCount = new PhotoCountSpecification(request);
        var totalCount = await unitOfWork.Repository<Photo>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<Photo>, IReadOnlyList<PhotoResponse>>(response);

        return new ApiResponse<PagedResponse<PhotoResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}