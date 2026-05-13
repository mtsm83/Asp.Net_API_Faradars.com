namespace Faradars.Application.DTOs.Payments.Order.OrderService;

public class OrderItemDto
{
    public int CourseId { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
}