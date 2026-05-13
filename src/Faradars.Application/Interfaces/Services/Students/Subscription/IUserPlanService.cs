using Faradars.Application.DTOs.Students.Subscription.UserPlanService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Subscription;

public interface IUserPlanService
{
    Task<Result<UserPlanDto>> AddUserPlanAsync(AddUserPlanDto dto, CancellationToken ct);
    Task<Result<UserPlanDto>> UpdateUserPlanAsync(UpdateUserPlanDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteUserPlanAsync(int userId, CancellationToken ct);
    Task<Result<List<UserPlanDto>>> GetAllUserPlansAsync(int userId, CancellationToken ct);
    
}