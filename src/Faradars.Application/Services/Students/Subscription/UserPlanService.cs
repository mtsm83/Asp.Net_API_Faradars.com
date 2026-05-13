using Faradars.Application.DTOs.Students.Subscription.UserPlanService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Subscription;
using Faradars.Application.Mappers.Students.Subscription;
using Faradars.Domain.Entities.Courses.Subscription;
using Faradars.Domain.Entities.Payments.Transaction;
using Faradars.Domain.Entities.Students.Subscription;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Enums;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Subscription;

public class UserPlanService(
    IRepository<User> userRepository,
    IRepository<UserPlan> userPlanRepository,
    IRepository<SubscriptionPlan> planRepository,
    IRepository<Transaction> transactionRepository,
    IUserContextService userContextService)
    : IUserPlanService, IScopedDependency
{
    public async Task<Result<UserPlanDto>> AddUserPlanAsync(AddUserPlanDto dto, CancellationToken ct)
    {
        var student = await userRepository.GetByIdAsync(ct, dto.StudentId);
        if (student == null)
            return Result.Failure<UserPlanDto>(Error.NotFound);
        var plan = await planRepository.GetByIdAsync(ct, dto.PlanId);
        if (plan == null)
            return Result.Failure<UserPlanDto>(Error.NotFound);
        var transaction = await transactionRepository.GetByIdAsync(ct, dto.PaymentTransactionId);
        if (transaction == null)
            return Result.Failure<UserPlanDto>(Error.NotFound);

        var hasActivePlan = await userPlanRepository.TableNoTracking
            .AnyAsync(p => p.UserId == dto.StudentId &&
                           p.Status == UserPlanStatus.Active, ct);
        if (hasActivePlan)
            return Result.Failure<UserPlanDto>(Error.AlreadyExists);
        var userPlan = dto.MapAddUserPlanDto(userContextService.CurrentUser.UserId);
        userPlan.StartsAt = DateTime.UtcNow;
        userPlan.Status = UserPlanStatus.Active;
        userPlan.CreatedBy = userContextService.CurrentUser.UserId;

        await userPlanRepository.AddAsync(userPlan, ct);

        var resultDto = userPlan.MapToUserPlanDto();
        return Result.Success(resultDto);
    }
    
    public async Task<Result<UserPlanDto>> UpdateUserPlanAsync(UpdateUserPlanDto dto, CancellationToken ct)
    {
        var userPlan = await userPlanRepository.GetByIdAsync(ct, dto.UserPlanId);
        if (userPlan == null)
            return Result.Failure<UserPlanDto>(Error.NotFound);
        userPlan.MapUpdateUserPlanDto(dto, userContextService.CurrentUser.UserId);
        userPlan.UpdatedAt = DateTime.UtcNow;
        userPlan.UpdatedBy = userContextService.CurrentUser.UserId;
        await userPlanRepository.UpdateAsync(userPlan, ct);
        var resultDto = userPlan.MapToUserPlanDto();
        return Result.Success(resultDto);
    }
    
    public async Task<Result<Unit>> DeleteUserPlanAsync(int userPlanId, CancellationToken ct)
    {
        var userPlan = await userPlanRepository.GetByIdAsync(ct, userPlanId);
        if (userPlan == null)
            return Result.Failure<Unit>(Error.NotFound);
        await userPlanRepository.DeleteAsync(userPlan, ct);
        return Result.Success(Unit.Value);
    }
    
    public async Task<Result<List<UserPlanDto>>> GetAllUserPlansAsync(int userId, CancellationToken ct)
    {
        var plans = await userPlanRepository.TableNoTracking
            .Where(p => p.UserId == userId)
            .ToListAsync(ct);
        if (plans.Count == 0)
            return Result.Failure<List<UserPlanDto>>(Error.NotFound);
        var result = plans.Select(p => p.MapToUserPlanDto()).ToList();
        return Result.Success(result);
    }
}