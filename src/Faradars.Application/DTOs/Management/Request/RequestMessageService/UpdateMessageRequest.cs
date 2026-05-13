namespace Faradars.Application.DTOs.Management.Request.RequestMessageService;

public class UpdateMessageRequest
{
    public int MessageId { get; set; }
    public string Body { get; set; } = null!;
    public int Order { get; set; }
    
}