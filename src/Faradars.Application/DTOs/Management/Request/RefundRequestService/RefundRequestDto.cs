namespace Faradars.Application.DTOs.Management.Request.RefundRequestService;

public class RefundRequestDto
{
    public int RefundRequestId { get; set; }
    public int UserRequestId { get; set; }
    public int OrderItemId { get; set; }
    public decimal? RefundAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}