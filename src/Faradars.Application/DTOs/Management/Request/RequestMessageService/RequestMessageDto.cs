using Faradars.Application.DTOs.Courses.Media.MediaService;

namespace Faradars.Application.DTOs.Management.Request.RequestMessageService;

public class RequestMessageDto
{
    public int MessageId { get; set; }
    public int RequestId { get; set; }
    public string Body { get; set; } = null!;
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public List<AssetFileDto> AssetFiles { get; set; } = new();
}