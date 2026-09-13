using System;
using MediatR;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Application.Models.Response.Photos;
using Woodshed.Application.Specifications.Photos;

namespace Woodshed.Application.Features.Account.Queries.GetPagePhotos;

public class GetPagedAccountPhotosQuery : PhotoSpecificationParams, IRequest<ApiResponse<PagedResponse<PhotoResponse>>>
{
    
}
