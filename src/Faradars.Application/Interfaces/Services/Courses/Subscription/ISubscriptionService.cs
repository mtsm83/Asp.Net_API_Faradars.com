using Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Subscription;

public interface ISubscriptionService
{
    Task<Result<SubscriptionDto>> AddSubscriptionAsync(AddSubscriptionDto dto, CancellationToken ct);
    Task<Result<SubscriptionDto>> UpdateSubscriptionAsync(UpdateSubscriptionDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteSubscriptionAsync(int subscriptionId, CancellationToken ct);
    
    Task<Result<SubscriptionDto>> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct);
    Task<Result<List<SubscriptionDto>>> GetAllSubscriptionsAsync(CancellationToken ct);

    Task<Result<Unit>> AssignCourseToSubscriptionAsync(int subscriptionId, int courseId, CancellationToken ct);
    Task<Result<Unit>> RemoveCourseThanSubscriptionAsync(int subscriptionId, int courseId, CancellationToken ct);

    Task<Result<Unit>> AssignTagToSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct);
    Task<Result<Unit>> RemoveTagThanSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct);

    Task<Result<Unit>> AssignCategoryToSubscriptionAsync(int subscriptionId, int categoryId, CancellationToken ct);
    Task<Result<Unit>> RemoveCategoryThanSubscriptionAsync(int subscriptionId, int categoryId, CancellationToken ct);
}