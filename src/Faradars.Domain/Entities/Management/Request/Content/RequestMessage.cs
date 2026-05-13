using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Management.Request.Content;

public class RequestMessage : BaseEntity
{
    public int RequestId { get; set; }
    public string Body { get; set; } = null!;
    public int Order { get; set; } //  (1 = user request body, 2 = admin, ...)

    public UserRequest Request { get; set; } = null!;
    public ICollection<MessageAsset> MessageAssets { get; set; } = new List<MessageAsset>();
}