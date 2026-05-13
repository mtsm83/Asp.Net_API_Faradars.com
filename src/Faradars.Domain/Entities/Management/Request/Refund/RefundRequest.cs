using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Domain.Entities.Payments.Order;

namespace Faradars.Domain.Entities.Management.Request.Refund;

public class RefundRequest : BaseEntity
{
    public int UserRequestId { get; set; }
    public int OrderItemId { get; set; }
    public decimal? RefundAmount { get; set; } = null!; // in Rial like 20,000,000 Rial

    public UserRequest UserRequest { get; set; } = null!;
    public OrderItem OrderItem { get; set; } = null!;
}