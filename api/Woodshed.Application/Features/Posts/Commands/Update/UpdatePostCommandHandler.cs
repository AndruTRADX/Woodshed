using System;
using AutoMapper;
using MediatR;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain;

namespace Woodshed.Application.Features.Posts.Commands.Update;

public class UpdatePostCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper) : IRequestHandler<UpdatePostCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault()
            ?? throw new UnauthorizedException();

        var response = await unitOfWork.Repository<Post>().GetFirstAsync(x => x.Id == request.Id)
            ?? throw new NotFoundException(nameof(Post), request.Id);

        response.PrepareUpdate(userId);

        var data = mapper.Map(request.Request, response);

        unitOfWork.Repository<Post>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
