using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Students.Subscription.UserPlanService;

public class UpdateUserPlanDto
{
    public int UserPlanId { get; set; }
    public UserPlanStatus? Status { get; set; }
    public int? RefundRequestId { get; set; }
}