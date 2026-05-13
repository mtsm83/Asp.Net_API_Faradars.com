using Faradars.Application.DTOs.Payments.Order.OrderService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Payments.Order;
using Faradars.Domain.Enums;
using Faradars.Shared.Result;

namespace Faradars.Application.Services.Payments.Order;

public class OrderService(
    IUserContextService userContextService,
    IRepository<Domain.Entities.Payments.Order.Order> orderRepository) : IOrderService
{
    public Task<Result<OrderDto>> CreateOrderFromCartAsync(int userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<Result<OrderDto>> IOrderService.AddOrderFromCartAsync(int userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDto>> GetOrderByIdAsync(int orderId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<OrderDto>>> GetUserOrdersAsync(int userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> ConfirmOrderAsync(int orderId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> DeleteOrderAsync(int orderId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<OrderDto>>> GetAllOrdersAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDto>> UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<OrderDto>>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}