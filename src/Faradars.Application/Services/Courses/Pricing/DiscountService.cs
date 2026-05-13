using Faradars.Application.DTOs.Courses.Pricing.DiscountService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.Application.Mappers.Courses.Pricing;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Pricing.Discount;
using Faradars.Domain.Entities.Payments.Order;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Pricing;

public class DiscountService(
    IRepository<Course> courseRepository,
    IRepository<Coupon> couponRepository,
    IRepository<CouponUsage> couponUsageRepository,
    IRepository<User> userRepository,
    IRepository<Order> orderRepository,
    IUserContextService userContextService) : IDiscountService, IScopedDependency
{
    public async Task<Result<CouponDto>> AddCouponAsync(AddCouponDto dto, CancellationToken ct)
    {
        var existingCoupon = await couponRepository.TableNoTracking
            .FirstOrDefaultAsync(c => c.Code == dto.Code, ct);
        if (existingCoupon != null)
            return Result.Failure<CouponDto>(Error.AlreadyExists);
        var newCoupon = dto.MapAddCouponDto();
        newCoupon.CreatedBy = userContextService.CurrentUser.UserId;
        await couponRepository.AddAsync(newCoupon, ct);
        var couponDto = newCoupon.MapToCouponDto();
        return Result.Success(couponDto);
    }

    public async Task<Result<CouponDto>> UpdateCouponAsync(UpdateCouponDto dto, CancellationToken ct)
    {
        var coupon = await couponRepository.GetByIdAsync(ct, dto.CouponId);
        if (coupon == null)
            return Result.Failure<CouponDto>(Error.NotFound);
        coupon.MapUpdateCouponDto(dto);
        coupon.UpdatedAt = DateTime.Now;
        coupon.UpdatedBy = userContextService.CurrentUser.UserId;
        await couponRepository.UpdateAsync(coupon, ct);
        var couponDto = coupon.MapToCouponDto();
        return Result.Success(couponDto);
    }

    public async Task<Result<CouponDto>> DeleteCouponAsync(int couponId, CancellationToken ct)
    {
        var coupon = await couponRepository.GetByIdAsync(ct, couponId);
        if (coupon == null)
            return Result.Failure<CouponDto>(Error.NotFound);
        await couponRepository.DeleteAsync(coupon, ct);
        var couponDto = coupon.MapToCouponDto();
        return Result.Success(couponDto);
    }

    public async Task<Result<CouponDto>> GetCouponByIdAsync(int couponId, CancellationToken ct)
    {
        var coupon = await couponRepository.GetByIdAsync(ct, couponId);
        if (coupon == null)
            return Result.Failure<CouponDto>(Error.NotFound);
        var couponDto = coupon.MapToCouponDto();
        return Result.Success(couponDto);
    }

    public async Task<Result<List<CouponDto>>> GetAllCouponsAsync(CancellationToken ct)
    {
        var coupons = await couponRepository.TableNoTracking.ToListAsync(ct);
        if (!coupons.Any())
            return Result.Failure<List<CouponDto>>(Error.NotFound);
        var couponDtos = coupons.Select(c => c.MapToCouponDto()).ToList();
        return Result.Success(couponDtos);
    }

    public async Task<Result<CouponUsageDto>> AddCouponUsageAsync(AddCouponUsageDto dto, CancellationToken ct)
    {
        var coupon = await couponRepository.GetByIdAsync(ct, dto.CouponId);
        if (coupon == null)
            return Result.Failure<CouponUsageDto>(Error.NotFound);
        
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<CouponUsageDto>(Error.NotFound);
        
        var order = await orderRepository.GetByIdAsync(ct, dto.OrderId);
        if (order == null)
            return Result.Failure<CouponUsageDto>(Error.NotFound);


        var newCouponUsage = dto.MapAddCouponUsage();
        newCouponUsage.CreatedBy = userContextService.CurrentUser.UserId;
        await couponUsageRepository.AddAsync(newCouponUsage, ct);
        var usageDto = newCouponUsage.MapToUsageDto();
        return Result.Success(usageDto);
    }

    public async Task<Result<Unit>> DeleteCouponUsageAsync(int usageId, CancellationToken ct)
    {
        var usage = await couponUsageRepository.GetByIdAsync(ct, usageId);
        if (usage == null)
            return Result.Failure<Unit>(Error.NotFound);
        await couponUsageRepository.DeleteAsync(usage, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<CouponUsageDto>> GetCouponUsageByIdAsync(int usageId, CancellationToken ct)
    {
        var usage = await couponUsageRepository.GetByIdAsync(ct, usageId);
        if (usage == null)
            return Result.Failure<CouponUsageDto>(Error.NotFound);
        var usageDto = usage.MapToUsageDto();
        return Result.Success(usageDto);
    }

    public async Task<Result<List<CouponUsageDto>>> GetAllUsagesAsync(CancellationToken ct)
    {
        var usages = await couponUsageRepository.TableNoTracking.ToListAsync(ct);
        if (!usages.Any())
            return Result.Failure<List<CouponUsageDto>>(Error.NotFound);
        var usageDtos = usages.Select(u => u.MapToUsageDto()).ToList();
        return Result.Success(usageDtos);
    }
}