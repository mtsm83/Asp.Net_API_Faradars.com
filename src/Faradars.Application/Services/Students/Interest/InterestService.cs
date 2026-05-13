using Faradars.Application.DTOs.Students.Interest.InterestService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Interest;
using Faradars.Application.Mappers.Students.Interest;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Students.Interest;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Interest;

public class InterestService(
    IRepository<User> userRepository,
    IRepository<Course> courseRepository,
    IRepository<InterestItem> interestRepository,
    IRepository<UserInterest> userInterestRepository,
    IRepository<WishlistItem> wishlistRepository,
    IUserContextService userContextService)
    : IInterestService, IScopedDependency
{
    public async Task<Result<InterestItemDto>> AddInterestItemAsync(AddInterestItemDto dto, CancellationToken ct)
    {
        var exists = await interestRepository.TableNoTracking
            .AnyAsync(x => x.Name == dto.Name, ct);

        if (exists)
            return Result.Failure<InterestItemDto>(Error.AlreadyExists);

        var entity = dto.MapAddInterestItemDto();
        entity.CreatedBy = userContextService.CurrentUser.UserId;

        await interestRepository.AddAsync(entity, ct);

        return Result.Success(entity.MapToInterestItemDto());
    }

    public async Task<Result<Unit>> DeleteInterestItemAsync(DeleteInterestItemDto dto, CancellationToken ct)
    {
        var entity = await interestRepository.GetByIdAsync(ct, dto.InterestItemId);
        if (entity == null)
            return Result.Failure<Unit>(Error.NotFound);

        await interestRepository.DeleteAsync(entity, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<InterestItemDto>> GetInterestItemByIdAsync(int interestId, CancellationToken ct)
    {
        var entity = await interestRepository.GetByIdAsync(ct, interestId);
        if (entity == null)
            return Result.Failure<InterestItemDto>(Error.NotFound);

        return Result.Success(entity.MapToInterestItemDto());
    }

    public async Task<Result<List<InterestItemDto>>> GetAllInterestItemsAsync(CancellationToken ct)
    {
        var items = await interestRepository.TableNoTracking.ToListAsync(ct);
        if (items.Count == 0)
            return Result.Failure<List<InterestItemDto>>(Error.NotFound);

        return Result.Success(items.Select(x => x.MapToInterestItemDto()).ToList());
    }

    public async Task<Result<WishListItemDto>> AddItemToWishListAsync(AddItemToWishListDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<WishListItemDto>(Error.NotFound);

        var course = await courseRepository.GetByIdAsync(ct, dto.CourseId);
        if (course == null)
            return Result.Failure<WishListItemDto>(Error.NotFound);

        var exists = await wishlistRepository.TableNoTracking
            .AnyAsync(x => x.UserId == dto.UserId && x.CourseId == dto.CourseId, ct);

        if (exists)
            return Result.Failure<WishListItemDto>(Error.AlreadyExists);

        var entity = dto.MapAddWishListItemDto();
        entity.CreatedBy = userContextService.CurrentUser.UserId;

        await wishlistRepository.AddAsync(entity, ct);

        return Result.Success(entity.MapToWishListItemDto());
    }

    public async Task<Result<Unit>> DeleteItemToWishListAsync(DeleteItemThanWishListDto dto, CancellationToken ct)
    {
        var entity = await wishlistRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.CourseId == dto.CourseId, ct);

        if (entity == null)
            return Result.Failure<Unit>(Error.NotFound);

        await wishlistRepository.DeleteAsync(entity, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<List<WishListItemDto>>> GetUserWishListAsync(int userId, CancellationToken ct)
    {
        var items = await wishlistRepository.TableNoTracking
            .Where(x => x.UserId == userId)
            .ToListAsync(ct);

        if (items.Count == 0)
            return Result.Failure<List<WishListItemDto>>(Error.NotFound);

        return Result.Success(items.Select(x => x.MapToWishListItemDto()).ToList());
    }
}