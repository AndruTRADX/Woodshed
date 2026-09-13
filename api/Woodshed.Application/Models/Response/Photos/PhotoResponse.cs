using System;

namespace Woodshed.Application.Models.Response.Photos;

public class PhotoResponse
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public required string PublicId { get; set; }
    public required string UserId { get; set; }
}
