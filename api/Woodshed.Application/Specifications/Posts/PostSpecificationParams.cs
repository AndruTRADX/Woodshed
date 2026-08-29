namespace Woodshed.Application.Specifications.Posts;

public class PostSpecificationParams : SpecificationParams
{
    public bool IsMyPost { get; set; }
    public string? UserId { get; set; }
}
