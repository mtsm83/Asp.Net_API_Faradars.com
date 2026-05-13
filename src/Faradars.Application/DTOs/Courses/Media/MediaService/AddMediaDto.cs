using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Media.MediaService;

public class AddMediaDto
{
    public IFormFile File { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? StorageProvider { get; set; }
    public long? Bytes { get; set; }
    public string? ContentType { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Resolution { get; set; }
}