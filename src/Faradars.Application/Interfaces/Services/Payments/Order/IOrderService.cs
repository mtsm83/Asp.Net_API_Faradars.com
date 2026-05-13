using Faradars.Application.DTOs.Payments.Order.OrderService;
using Faradars.Domain.Enums;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Order;

public interface IOrderService
{
    Task<Result<OrderDto>> AddOrderFromCartAsync(int userId, CancellationToken ct);
    Task<Result<OrderDto>> GetOrderByIdAsync(int orderId, CancellationToken ct);
    Task<Result<List<OrderDto>>> GetUserOrdersAsync(int userId, CancellationToken ct);
    Task<Result<bool>> ConfirmOrderAsync(int orderId, CancellationToken ct);
    Task<Result<Unit>> DeleteOrderAsync(int orderId, CancellationToken ct); // same as cancelling one's order
    Task<Result<List<OrderDto>>> GetAllOrdersAsync(CancellationToken ct);
    Task<Result<OrderDto>> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken ct);
    Task<Result<List<OrderDto>>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct);
}