namespace Faradars.Application.DTOs.Courses.Media.MediaService;

public class DownloadMediaDto
{
    public string FileName { get; set; } = null!;
    public byte[] FileContent { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long FileSize { get; set; }
}