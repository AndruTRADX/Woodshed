using Woodshed.Domain;

namespace Woodshed.Application.Specifications.Photos;

public class PhotoSpecification : BaseSpecification<Photo>
{
    public PhotoSpecification(PhotoSpecificationParams specParams) : base(
        x =>
            string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Contains(specParams.UserId)
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
}
