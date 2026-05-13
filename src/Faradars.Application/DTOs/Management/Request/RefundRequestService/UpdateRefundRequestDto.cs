namespace Faradars.Application.DTOs.Management.Request.RefundRequestService;

public class UpdateRefundRequestDto
{
    public int RefundId { get; set; }
    public int OrderItemId { get; set; }
    public decimal? RefundAmount { get; set; } = null!; // in Rial like 20,000,000 Rial
}