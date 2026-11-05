using Woodshed.Domain;

namespace Woodshed.Application.Specifications.Photos;

public class PhotoCountSpecification(PhotoSpecificationParams specParams) : BaseSpecification<Photo>(
    x =>
        string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId == specParams.UserId
    )
{ }