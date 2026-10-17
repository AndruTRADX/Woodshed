namespace Woodshed.Application.Models.Videos;

public class VideoUploadResults
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public double DurationSeconds { get; set; }
}
