using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Management.Request.RequestMessageService;

public class AddRequestMessage
{
    public int RequestId { get; set; }
    public string Body { get; set; } = null!;
    public int Order { get; set; }
    public IFormFile? UploadedFiles { get; set; }
}