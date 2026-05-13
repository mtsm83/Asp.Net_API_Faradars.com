using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Domain.Entities.Management.Request.Content;

public class MessageAsset: BaseEntity
{
    public int MessageId { get; set; }
    public int AssetFileId { get; set; }

    public RequestMessage Message { get; set; } = null!;
    public AssetFile AssetFile { get; set; } = null!;
}