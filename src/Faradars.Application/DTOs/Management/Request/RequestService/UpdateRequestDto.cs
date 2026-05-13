using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Management.Request.RequestService;

public class UpdateRequestDto
{
    public int RequestId { get; set; }
    public int? SubjectId { get; set; }
    public RequestStatus? Status { get; set; } = RequestStatus.Pending;
    public DateTime? CompletedAt { get; set; }
}