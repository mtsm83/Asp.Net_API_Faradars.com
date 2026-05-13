using Faradars.Application.DTOs.Courses.Pricing.DiscountService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Pricing;

public interface IDiscountService
{
    Task<Result<CouponDto>> AddCouponAsync(AddCouponDto dto, CancellationToken ct);
    Task<Result<CouponDto>> UpdateCouponAsync(UpdateCouponDto dto, CancellationToken ct);
    Task<Result<CouponDto>> DeleteCouponAsync(int couponId, CancellationToken ct);
    Task<Result<CouponDto>> GetCouponByIdAsync(int couponId, CancellationToken ct);
    Task<Result<List<CouponDto>>> GetAllCouponsAsync(CancellationToken ct);    
    
    Task<Result<CouponUsageDto>> AddCouponUsageAsync(AddCouponUsageDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteCouponUsageAsync(int usageId, CancellationToken ct);
    Task<Result<CouponUsageDto>> GetCouponUsageByIdAsync(int usageId, CancellationToken ct);
    Task<Result<List<CouponUsageDto>>> GetAllUsagesAsync(CancellationToken ct);
    
    // todo: For reporting matters, APIs like (all users who used X, all used X of the user Y), etc
}