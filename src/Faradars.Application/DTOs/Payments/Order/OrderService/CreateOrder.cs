namespace Faradars.Application.DTOs.Payments.Order.OrderService;

public class CreateOrder
{
    public int UserId { get; init; }
    public List<int> CourseIds { get; init; }
    public decimal Total { get; init; }
}