using Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Subscription;
using Faradars.Application.Mappers.Courses.Subsctiption;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Subscription;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Subscription;

public class SubscriptionService(
    IRepository<SubscriptionPlan> planRepository,
    IRepository<Course> courseRepository,
    IRepository<SubscriptionCourse> planCourseRepository,
    IUserContextService userContextService) : ISubscriptionService, IScopedDependency
{
    public async Task<Result<SubscriptionDto>> AddSubscriptionAsync(AddSubscriptionDto dto, CancellationToken ct)
    {
        var newPlan = dto.MapAddSubscription();
        newPlan.CreatedBy = userContextService.CurrentUser.UserId;
        await planRepository.AddAsync(newPlan, ct);
        var planDto = newPlan.MapToPlanDto();
        return Result.Success(planDto);
    }

    public async Task<Result<SubscriptionDto>> UpdateSubscriptionAsync(UpdateSubscriptionDto dto, CancellationToken ct)
    {
        var plan = await planRepository.GetByIdAsync(ct, dto.PlanId);
        if (plan == null)
            return Result.Failure<SubscriptionDto>(Error.NotFound);
        plan.MapUpdateSubscriptionDto(dto);
        plan.UpdatedAt = DateTime.Now;
        plan.UpdatedBy = userContextService.CurrentUser.UserId;
        await planRepository.UpdateAsync(plan, ct);
        var planDto = plan.MapToPlanDto();
        return Result.Success(planDto);
    }

    public async Task<Result<Unit>> DeleteSubscriptionAsync(int subscriptionId, CancellationToken ct)
    {
        var plan = await planRepository.GetByIdAsync(ct, subscriptionId);
        if (plan == null)
            return Result.Failure<Unit>(Error.NotFound);
        await planRepository.DeleteAsync(plan, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<SubscriptionDto>> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct)
    {
        var plan = await planRepository.GetByIdAsync(ct, subscriptionId);
        if (plan == null)
            return Result.Failure<SubscriptionDto>(Error.NotFound);
        var planDto = plan.MapToPlanDto();
        return Result.Success(planDto);
    }

    public async Task<Result<List<SubscriptionDto>>> GetAllSubscriptionsAsync(CancellationToken ct)
    {
        var plans = await planRepository.TableNoTracking.ToListAsync(ct);
        if (plans.Count == 0)
            return Result.Failure<List<SubscriptionDto>>(Error.NotFound);
        var planDtos = plans.Select(p => p.MapToPlanDto()).ToList();
        return Result.Success(planDtos);
    }

    public async Task<Result<Unit>> AssignCourseToSubscriptionAsync(int subscriptionId, int courseId,
        CancellationToken ct)
    {
        var course = await courseRepository.GetByIdAsync(ct, courseId);
        if (course == null)
            return Result.Failure<Unit>(Error.NotFound);

        var existingAssignment = await planCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(oc =>
                oc.CourseId == courseId && oc.PlanId == subscriptionId, ct);
        if (existingAssignment != null)
            return Result.Failure<Unit>(Error.AlreadyExists);

        var newOfferCourse = new SubscriptionCourse
        {
            CourseId = courseId,
            PlanId = subscriptionId,
            CreatedBy = userContextService.CurrentUser.UserId
        };
        await planCourseRepository.AddAsync(newOfferCourse, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> RemoveCourseThanSubscriptionAsync(int subscriptionId, int courseId,
        CancellationToken ct)
    {
        var assignment = await planCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(oc =>
                oc.CourseId == courseId && oc.PlanId == subscriptionId, ct);
        if (assignment == null)
            return Result.Failure<Unit>(Error.NotFound);
        await planCourseRepository.DeleteAsync(assignment, ct);
        return Result.Success(Unit.Value);
    }

    public Task<Result<Unit>> AssignTagToSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> RemoveTagThanSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> AssignCategoryToSubscriptionAsync(int subscriptionId, int categoryId,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> RemoveCategoryThanSubscriptionAsync(int subscriptionId, int categoryId,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}