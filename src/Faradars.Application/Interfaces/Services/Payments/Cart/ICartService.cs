using Faradars.Application.DTOs.Payments.Cart.CartService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Cart;

public interface ICartService
{
    Task<Result<CartDto>> AddCartForUserAsync(int userId, CancellationToken ct); // must be called when a user is registered
    Task<Result<Unit>> DeleteCartForUserAsync(int userId, CancellationToken ct); // must be called when a user is deleted
    Task<Result<CartDto>> AddItemToCartAsync(AddToCartDto dto, CancellationToken ct);
    Task<Result<CartDto>> RemoveFromCartAsync(RemoveFromCartDto dto, CancellationToken ct);
    Task<Result<CartDto>> GetUserCartAsync(int userId, CancellationToken ct);
}