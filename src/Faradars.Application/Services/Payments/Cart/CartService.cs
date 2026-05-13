using Faradars.Application.DTOs.Payments.Cart.CartService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Payments.Cart;
using Faradars.Application.Mappers.Payments.Cart;
using Faradars.Domain.Entities.Payments.Cart;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Payments.Cart;

public class CartService(
    IUserContextService userContextService,
    IRepository<CartItem> cartItemRepository,
    IRepository<Domain.Entities.Payments.Cart.Cart> cartRepository
) : ICartService, IScopedDependency
{
    public async Task<Result<CartDto>> AddCartForUserAsync(int userId, CancellationToken ct)
    {
        var existingCart = await cartRepository.TableNoTracking
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ConvertedToOrderAt == null, ct);
        if (existingCart != null)
            return Result.Failure<CartDto>(Error.AlreadyExists);

        var newCart = new Domain.Entities.Payments.Cart.Cart
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userContextService.CurrentUser.UserId
        };
        await cartRepository.AddAsync(newCart, ct);
        var dto = newCart.MapToCartDto();
        return Result.Success(dto);
    }

    public async Task<Result<Unit>> DeleteCartForUserAsync(int userId, CancellationToken ct)
    {
        var cart = await cartRepository.TableNoTracking
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ConvertedToOrderAt == null, ct);
        if (cart == null)
            return Result.Failure<Unit>(Error.NotFound);
        await cartRepository.DeleteAsync(cart, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<CartDto>> AddItemToCartAsync(AddToCartDto dto, CancellationToken ct)
    {
        var cart = await cartRepository.TableNoTracking
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == dto.CartId && c.ConvertedToOrderAt == null, ct);
        if (cart == null)
            return Result.Failure<CartDto>(Error.NotFound);

        var existingItem = cart.Items?.FirstOrDefault(i => i.ItemId == dto.ItemId);
        if (existingItem == null)
            return Result.Failure<CartDto>(Error.AlreadyExists);
        
        await cartItemRepository.UpdateAsync(existingItem, ct);
        var newItem = new CartItem
        {
            CartId = cart.Id,
            ItemId = dto.ItemId,
            CartOrderItemTypeId = dto.CartOrderItemTypeId,
        };
        await cartItemRepository.AddAsync(newItem, ct);

        var cartDto = cart.MapToCartDto();
        return Result.Success(cartDto);
    }

    public async Task<Result<CartDto>> RemoveFromCartAsync(RemoveFromCartDto dto, CancellationToken ct)
    {
        var cart = await cartRepository.TableNoTracking
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == dto.CartId && c.ConvertedToOrderAt == null, ct);

        if (cart == null)
            return Result.Failure<CartDto>(Error.NotFound);

        var item = cart.Items?.FirstOrDefault(i => i.Id == dto.ItemId);
        if (item == null)
            return Result.Failure<CartDto>(Error.NotFound);

        await cartItemRepository.DeleteAsync(item, ct);

        var updatedCart = await cartRepository.TableNoTracking
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cart.Id, ct);

        var cartDto = cart.MapToCartDto();
        return Result.Success(cartDto);
    }

    public async Task<Result<CartDto>> GetUserCartAsync(int userId, CancellationToken ct)
    {
        var cart = await cartRepository.TableNoTracking
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ConvertedToOrderAt == null, ct);

        if (cart == null)
            return Result.Failure<CartDto>(Error.NotFound);

        var cartDto = cart.MapToCartDto();
        return Result.Success(cartDto);
    }
}