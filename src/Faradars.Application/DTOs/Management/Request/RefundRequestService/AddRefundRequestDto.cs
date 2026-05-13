namespace Faradars.Application.DTOs.Management.Request.RefundRequestService;

public class AddRefundRequestDto
{
    public int RequestId { get; set; }
    public int OrderItemId { get; set; }
    public decimal? RefundAmount { get; set; } = null!; // in Rial like 20,000,000 Rial
}