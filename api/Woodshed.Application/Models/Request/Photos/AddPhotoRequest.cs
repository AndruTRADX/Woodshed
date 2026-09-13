using Microsoft.AspNetCore.Http;

namespace Woodshed.Application.Models.Request.Photos;

public class AddPhotoRequest
{
    public required IFormFile File { get; set; }
}
