namespace Faradars.Application.DTOs.Courses.Media.MediaService;

public class AssetFileDto
{
    public int Id { get; set; }
    public string MediaType { get; set; } = null!; // video, image
    public string ContentType { get; set; } = null!; // mp4 , jpeg
    public string OriginalFileName { get; set; } = null!;
    public string StoredFileName { get; set; } = null!;
    public long Size { get; set; } // 5MB to download
    public string Path { get; set; } = null!; // url
    public string? Description { get; set; }
    public string? Resolution { get; set; } // 1080p
    public TimeSpan? Duration { get; set; }
    
    public int CreatorId { get; set; }
    public int? UpdaterId { get; set; }
    public int? DeleterId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}