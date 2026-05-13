using Faradars.Application.DTOs.Students.Interest.InterestService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Interest;

public interface IInterestService
{
    Task<Result<InterestItemDto>> AddInterestItemAsync(AddInterestItemDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteInterestItemAsync(DeleteInterestItemDto dto, CancellationToken ct);
    Task<Result<InterestItemDto>> GetInterestItemByIdAsync(int interestId, CancellationToken ct);
    Task<Result<List<InterestItemDto>>> GetAllInterestItemsAsync(CancellationToken ct);
    
    Task<Result<WishListItemDto>> AddItemToWishListAsync(AddItemToWishListDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteItemToWishListAsync(DeleteItemThanWishListDto dto, CancellationToken ct);
    Task<Result<List<WishListItemDto>>> GetUserWishListAsync(int userId, CancellationToken ct);

}